using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaModels;
using RAServices.DAL;
using RAServices.Model;
using System.Xml.Linq;

namespace RAServices.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private string? connectionString;
        private string? dbName;
        private MongoRepository _mongoRepository ;
        public PersonsController(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("MongoDb");
            dbName = configuration.GetValue<string>("DbName");
            _mongoRepository = new MongoRepository(connectionString, dbName, "Person");
        }
        [HttpPost]
        public async Task<ResponseModel<Person>> GetOne(RequestModel<Person> person)
        {
            var result = new ResponseModel<Person>();

            var postData = new DbModel<Person>()
            {
                Item = person.Item,
                RecordID = person.RecordId
            };

            var data = await _mongoRepository.GetFindOne<Person>(postData);
            result.Item = data.Item;
            result.RequestState = data.State;
            result.TotalRowCount = data.TotalRowCount;
            result.ErrorMsg = data.ErrorMsg;
            result.ServiceState = true;
            
            return result;
        }

        [HttpPost]
        public async Task<ResponseModel<Person>> GetList(RequestModel<Person> person)
        {
            var result = new ResponseModel<Person>();
            var postData = new DbModel<Person>()
            {
                Item = person.Item,
                RecordID = person.RecordId,
                PagingStart = person.PagingStart,
                PagingLength = person.PagingLength
            };

            var data = await _mongoRepository.GetDataList<Person>(postData);
            result.ItemList = data.ResultList;
            result.RequestState = data.State;
            result.TotalRowCount = data.TotalRowCount;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;
            return result;
        }

        [HttpPost]
        public async Task<ResponseModel<Person>> Create(RequestModel<Person> person)
        {
            var result = new ResponseModel<Person>();
            var postData = new DbModel<Person>()
            {
                Item = person.Item,
                RecordID = person.RecordId
            };

            var data = await _mongoRepository.InsertData<Person>(postData);
            
            result.Item = data.Item;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;

            return result;
        }

        [HttpPost]
        public async Task<ResponseModel<Person>> Update(RequestModel<Person> person)
        {
            var result = new ResponseModel<Person>();
            var postData = new DbModel<Person>()
            {
                Item = person.Item,
                RecordID = person.RecordId
            };
            var data = await _mongoRepository.UpdateData<Person>(postData);
            result.Item = data.Item;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;

            return result;
        }

        [HttpPost]
        public async Task<ResponseModel<Person>> Delete(RequestModel<Person> person)
        {
            var result = new ResponseModel<Person>();
            var postData = new DbModel<Person>()
            {
                Item = person.Item,
                RecordID = person.RecordId
            };
            var data = await _mongoRepository.DeleteData<Person>(postData);
            result.Item = null;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;

            return result;
        }
    }
}
