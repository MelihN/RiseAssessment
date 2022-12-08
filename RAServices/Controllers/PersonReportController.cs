using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaModels;
using RAServices.DAL;
using RAServices.Model;
using System;
using System.Net.Http.Headers;

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
                        
            var data = await GetPersonList(new RequestModel<Person>());

            if (data.RequestState)
            {
                var reportList = new List<PersonReport>();
                foreach (var person in data.ItemList)
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

            result.RequestState = data.RequestState;
            result.TotalRowCount = data.TotalRowCount;
            result.ErrorMsg = data.ErrorMsg;            
            result.ServiceState = true;

            return result;
        }

        private async Task<ResponseModel<Person>> GetPersonList(RequestModel<Person> person)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Host.Value);
            client.Timeout = TimeSpan.FromMinutes(5);
            byte[] cred = System.Text.UTF8Encoding.UTF8.GetBytes("RaTestUser:p@ssw0rdRa");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            

            using (HttpResponseMessage response = await client.PostAsJsonAsync("api/Person/GetList", person))
            {
                var result = new ResponseModel<Person>();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ResponseModel<Person>>();
                }                
                return result;
            }
        }
    }
}
