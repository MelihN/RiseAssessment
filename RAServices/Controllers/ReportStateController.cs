using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaModels;
using RAServices.DAL;
using RAServices.Model;

namespace RAServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportStateController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private string? connectionString;
        private string? dbName;
        private MongoRepository _mongoRepository;
        public ReportStateController(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("MongoDb");
            dbName = configuration.GetValue<string>("DbName");
            _mongoRepository = new MongoRepository(connectionString, dbName, "PersonReport");
        }
        [HttpPost]
        public async Task<ResponseModel<ReportQueryInfo>> GetOne(ReportQueryInfo model)
        {
            var result = new ResponseModel<ReportQueryInfo>();
            var postData = new DbModel<ReportQueryInfo>() { RecordID = model.UUID };

            var data = await _mongoRepository.GetFindOne<ReportQueryInfo>(postData);

            result.Item = data.Item;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;

            return result;
        }
        [HttpPost]
        public async Task<ResponseModel<ReportQueryInfo>> GetList(RequestModel<ReportQueryInfo> model)
        {
            var result = new ResponseModel<ReportQueryInfo>();
            var postData = new DbModel<ReportQueryInfo>() {  PagingStart = model.PagingStart, PagingLength = model.PagingLength };

            var data = await _mongoRepository.GetDataList<ReportQueryInfo>(postData);

            result.Item = data.Item;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;

            return result;
        }

        [HttpPost]
        public async Task<ResponseModel<ReportQueryInfo>> Create(ReportQueryInfo model)
        {
            var result = new ResponseModel<ReportQueryInfo>();
            var postData = new DbModel<ReportQueryInfo>() { Item = model };

            var data = await _mongoRepository.InsertData<ReportQueryInfo>(postData);

            result.Item = data.Item;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;

            return result;
        }

        [HttpPost]
        public async Task<ResponseModel<ReportQueryInfo>> Update(ReportQueryInfo model)
        {
            var result = new ResponseModel<ReportQueryInfo>();
            var postData = new DbModel<ReportQueryInfo>()
            {
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
