using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaModels;
using RAServices.DAL;
using RAServices.Model;
using System;

namespace RAServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonReportController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private string? connectionString;
        private string? dbName;
        private MongoRepository _mongoRepository;
        public PersonReportController(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("MongoDb");
            dbName = configuration.GetValue<string>("DbName");
            _mongoRepository = new MongoRepository(connectionString, dbName, "PersonReport");
        }

        [HttpPost]
        public async Task<ResponseModel<PersonReport>> GetReportData()
        {
            var result = new ResponseModel<PersonReport>();
            var postData = new DbModel<Person>();
            var reportStateInfo = await SetReportState(new ReportQueryInfo() { 
                UUID = new string(Guid.NewGuid().ToString()), 
                ReportState =ReportQueryStates.reportQueryStates.First(x => x.StateValue == "S1").StateName, 
                RequestDate = DateTime.Now });

            var mongoRepo = new MongoRepository(connectionString, dbName, "Person");
            var data = await mongoRepo.GetDataList<Person>(postData);

            reportStateInfo.Item.ReportState = ReportQueryStates.reportQueryStates.First(x => x.StateValue == "S2").StateName;
            reportStateInfo.Item.RequestDate = DateTime.Now;
            reportStateInfo = await UpdateReportState(reportStateInfo.Item);

            if (data.State)
            {
                var reportList = new List<PersonReport>();
                foreach (var person in data.ResultList)
                {
                    if (person.ContactInfos != null && person.ContactInfos.Count > 0)
                    {
                        foreach (var cInfo in person.ContactInfos)
                        {
                            if (cInfo != null && !string.IsNullOrWhiteSpace(cInfo.Location))
                            {
                                reportList.Add(new PersonReport() { Location = cInfo.Location, 
                                    NumberOfPerson = 1, 
                                    NumberOfPhoneNumber = (!string.IsNullOrWhiteSpace(cInfo.PhoneNumber) ? 1 : 0) });
                            }
                        }
                    }
                }

                result.ItemList = reportList.GroupBy(g => g.Location)
                    .Select(s => new PersonReport { Location = s.Key, 
                        NumberOfPerson = s.Sum(t1 => t1.NumberOfPerson), 
                        NumberOfPhoneNumber = s.Sum(t2 => t2.NumberOfPhoneNumber) }).ToList();
            }
            else
                result.ItemList = new List<PersonReport>();

            result.RequestState = data.State;
            result.TotalRowCount = data.TotalRowCount;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;

            reportStateInfo.Item.ReportState = ReportQueryStates.reportQueryStates.First(x => x.StateValue == "S3").StateName;
            reportStateInfo.Item.RequestDate = DateTime.Now;
            reportStateInfo = await UpdateReportState(reportStateInfo.Item);

            return result;
        }

        private async Task<ResponseModel<ReportQueryInfo>> SetReportState(ReportQueryInfo model)
        {
            var result = new ResponseModel<ReportQueryInfo>();
            var postData = new DbModel<ReportQueryInfo>(){ Item = model };

            var data = await _mongoRepository.InsertData<ReportQueryInfo>(postData);

            result.Item = data.Item;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;

            return result;
        }

        private async Task<ResponseModel<ReportQueryInfo>> UpdateReportState(ReportQueryInfo model)
        {
            var result = new ResponseModel<ReportQueryInfo>();
            var postData = new DbModel<ReportQueryInfo>() { 
                Item = model, 
                RecordID = model.UUID
            };

            var data = await _mongoRepository.UpdateData<ReportQueryInfo>(postData);

            result.Item = data.Item;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;

            return result;
        }
    }
}
