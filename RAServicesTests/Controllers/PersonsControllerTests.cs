using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver.Core.Configuration;
using RaModels;
using RAServices.Controllers;
using RAServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RAServices.Controllers.Tests
{
    [TestClass()]
    public class PersonsControllerTests
    {
        private readonly IConfiguration configuration;
        public PersonsControllerTests(IConfiguration configuration)
        {
            this.configuration = configuration;            
        }

        [TestMethod()]
        public async void GetOneTest()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:49155");
            client.Timeout = TimeSpan.FromMinutes(5);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage serviceresponse = await client.PostAsJsonAsync("api/Persons/GetOne", new RaModels.RequestModel<RaModels.Person>()))
            {
                var result = new ResponseModel<Person>();
                if (serviceresponse.IsSuccessStatusCode)
                {
                    result = await serviceresponse.Content.ReadFromJsonAsync<ResponseModel<Person>>();
                    Assert.IsTrue(result.RequestState);
                }
                else
                {
                    Assert.Fail(result.ErrorMsg);
                }                
            }
        }

        [TestMethod()]
        public async void GetListTest()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:49155");
            client.Timeout = TimeSpan.FromMinutes(5);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage serviceresponse = await client.PostAsJsonAsync("api/Persons/GetList", new RaModels.RequestModel<RaModels.Person>()))
            {
                var result = new ResponseModel<Person>();
                if (serviceresponse.IsSuccessStatusCode)
                {
                    result = await serviceresponse.Content.ReadFromJsonAsync<ResponseModel<Person>>();
                    Assert.IsTrue(result.RequestState);
                }
                else
                {
                    Assert.Fail(result.ErrorMsg);
                }
            }
        }

        [TestMethod()]
        public async void CreateTest()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:49155");
            client.Timeout = TimeSpan.FromMinutes(5);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var person = new RaModels.Person()
            {
                UUID = "02",
                Name = "Name2",
                Surname = "Surname2",
                CompanyName = "CompanyName2",
                ContactInfos = new List<RaModels.ContactInfo>() {
                     new RaModels.ContactInfo(){ Location="Ankara", Email="name1@rise.com", PhoneNumber="903123212121"}
                    }
            };

            using (HttpResponseMessage serviceresponse = await client.PostAsJsonAsync("api/Persons/Create", new RaModels.RequestModel<RaModels.Person>() { Item = person }))
            {
                var result = new ResponseModel<Person>();
                if (serviceresponse.IsSuccessStatusCode)
                {
                    result = await serviceresponse.Content.ReadFromJsonAsync<ResponseModel<Person>>();
                    Assert.IsTrue(result.RequestState);
                }
                else
                {
                    Assert.Fail(result.ErrorMsg);
                }
            }
        }

        [TestMethod()]
        public async void UpdateTest()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:49155");
            client.Timeout = TimeSpan.FromMinutes(5);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var person = new RaModels.Person()
            {
                UUID = "02",
                Name = "Name2",
                Surname = "Surname2",
                CompanyName = "CompanyName2",
                ContactInfos = new List<RaModels.ContactInfo>() {
                     new RaModels.ContactInfo(){ Location="Ankara", Email="name1@rise.com", PhoneNumber="903123212121"}
                    }
            };

            using (HttpResponseMessage serviceresponse = await client.PostAsJsonAsync("api/Persons/Update", new RaModels.RequestModel<RaModels.Person>() { Item = person }))
            {
                var result = new ResponseModel<Person>();
                if (serviceresponse.IsSuccessStatusCode)
                {
                    result = await serviceresponse.Content.ReadFromJsonAsync<ResponseModel<Person>>();
                    Assert.IsTrue(result.RequestState);
                }
                else
                {
                    Assert.Fail(result.ErrorMsg);
                }
            }
        }

        [TestMethod()]
        public async void DeleteTest()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:49155");
            client.Timeout = TimeSpan.FromMinutes(5);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (HttpResponseMessage serviceresponse = await client.PostAsJsonAsync("api/Persons/Update", new RaModels.RequestModel<RaModels.Person>() {  RecordId = "02" }))
            {
                var result = new ResponseModel<Person>();
                if (serviceresponse.IsSuccessStatusCode)
                {
                    result = await serviceresponse.Content.ReadFromJsonAsync<ResponseModel<Person>>();
                    Assert.IsTrue(result.RequestState);
                }
                else
                {
                    Assert.Fail(result.ErrorMsg);
                }
            }
        }
    }
}