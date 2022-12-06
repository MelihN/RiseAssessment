using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaModels;
using RAServices.DAL;
using RAServices.Model;

namespace RAServices.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private string? connectionString;
        private string dbName = "RaAssessment";
        private string collectionName = "Persons";        
        private MongoDbAccess _mongoDbAccess = new MongoDbAccess();
        public PersonsController(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("MongoDb");
        }
        [HttpPost]
        public ResponseModel<Person> GetOne(RequestModel<Person> person)
        {
            var result = new ResponseModel<Person>();

            var postData = new DbModel<Person>()
            {
                CollectionName = collectionName,
                ConnectionString = connectionString,
                DbName = dbName,
                Item = person.Item,
                RecordID = person.RecordId
            };

            var data = _mongoDbAccess.GetFindOne<Person>(postData);
            result.Item = data.Item;
            result.RequestState = data.State;
            result.TotalRowCount = data.TotalRowCount;
            result.ErrorMsg = data.ErrorMsg;
            result.ServiceState = true;
            
            return result;
        }

        [HttpPost]
        public ResponseModel<Person> GetList(RequestModel<Person> person)
        {
            var result = new ResponseModel<Person>();
            var postData = new DbModel<Person>()
            {
                CollectionName = collectionName,
                ConnectionString = connectionString,
                DbName = dbName,
                Item = person.Item,
                RecordID = person.RecordId,
                PagingStart = person.PagingStart,
                PagingLength = person.PagingLength

            };

            var data = _mongoDbAccess.GetDataList<Person>(postData);
            result.ItemList = data.ResultList;
            result.RequestState = data.State;

            result.TotalRowCount = data.TotalRowCount;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;
            return result;
        }

        [HttpPost]
        public ResponseModel<Person> Create(RequestModel<Person> person)
        {
            var result = new ResponseModel<Person>();

            var postData = new DbModel<Person>()
            {
                CollectionName = collectionName,
                ConnectionString = connectionString,
                DbName = dbName,
                Item = person.Item,
                RecordID = person.RecordId
            };

            var data = _mongoDbAccess.InsertData<Person>(postData);
            
            result.Item = data.Item;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;

            return result;
        }

        [HttpPost]
        public ResponseModel<Person> Update(RequestModel<Person> person)
        {
            var result = new ResponseModel<Person>();
            var postData = new DbModel<Person>()
            {
                CollectionName = collectionName,
                ConnectionString = connectionString,
                DbName = dbName,
                Item = person.Item,
                RecordID = person.RecordId
            };
            var data = _mongoDbAccess.UpdateData<Person>(postData);
            result.Item = data.Item;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;

            return result;
        }

        [HttpPost]
        public ResponseModel<Person> Delete(RequestModel<Person> person)
        {
            var result = new ResponseModel<Person>();
            var postData = new DbModel<Person>()
            {
                CollectionName = collectionName,
                ConnectionString = connectionString,
                DbName = dbName,
                Item = person.Item,
                RecordID = person.RecordId
            };
            var data = _mongoDbAccess.DeleteData<Person>(postData);
            result.Item = null;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;

            return result;
        }
    }
}
