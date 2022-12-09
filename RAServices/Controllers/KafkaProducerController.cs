using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Configuration;
using RaModels;
using RAServices.DAL;
using RAServices.Model;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace RAServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KafkaProducerController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private string? connectionString;
        private string? dbName;
        private MongoRepository _mongoRepository;
        private readonly string bootstrapServers;
        private readonly string topic = "PersonReport";
        public KafkaProducerController(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("MongoDb");
            dbName = configuration.GetValue<string>("DbName");
            bootstrapServers = configuration.GetValue<string>("KafkaUrl");
            _mongoRepository = new MongoRepository(connectionString, dbName, "personreport");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RequestModel<PersonReport> personReport)
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
                                reportList.Add(new PersonReport()
                                {
                                    Location = cInfo.Location,
                                    NumberOfPerson = 1,
                                    NumberOfPhoneNumber = (!string.IsNullOrWhiteSpace(cInfo.PhoneNumber) ? 1 : 0)
                                });
                            }
                        }
                    }
                }

                result.ItemList = reportList.GroupBy(g => g.Location)
                    .Select(s => new PersonReport
                    {
                        Location = s.Key,
                        NumberOfPerson = s.Sum(t1 => t1.NumberOfPerson),
                        NumberOfPhoneNumber = s.Sum(t2 => t2.NumberOfPhoneNumber)
                    }).ToList();
            }
            else
                result.ItemList = new List<PersonReport>();

            result.RequestState = data.RequestState;
            result.TotalRowCount = data.TotalRowCount;
            result.ErrorMsg = data.ErrorMsg;
            result.ServiceState = true;

            string message = JsonSerializer.Serialize(result);
            return Created(string.Empty, SendToKafka(topic, message));
        }
        private async Task<DeliveryResult<string, string>> SendToKafka(string topic, string messageBody)
        {
            var config = new ProducerConfig() { BootstrapServers = bootstrapServers };

            using (var producer = new ProducerBuilder<string,string>(config).Build())
            {
                try
                {
                    Message<string, string> message = new Message<string, string> { Value = messageBody };
                    DeliveryResult<string, string> result = producer.ProduceAsync(topic, message).GetAwaiter().GetResult();
                    return result;
                }
                catch(Exception ex)
                {
                    throw ex;
                }   
            }
        }

        private async Task<ResponseModel<Person>> GetPersonList(RequestModel<Person> person)
        {
            var result = new ResponseModel<Person>();
            MongoRepository repo1 = new MongoRepository(connectionString, dbName, "Person");
            var postData = new DbModel<Person>();
            var data = await repo1.GetDataList<Person>(postData);
            result.ItemList = data.ResultList;
            result.RequestState = data.State;
            result.TotalRowCount = data.TotalRowCount;
            result.ErrorMsg = data.ErrorMsg;
            result.RequestState = data.State;
            result.ServiceState = true;
            return result;
        }
    }
}
