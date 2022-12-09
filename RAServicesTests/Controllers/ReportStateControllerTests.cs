using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaModels;
using RAServices.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RAServices.Controllers.Tests
{
    [TestClass()]
    public class ReportStateControllerTests
    {
        [TestMethod()]
        public async void GetOneTest()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:49155");
            client.Timeout = TimeSpan.FromMinutes(5);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage serviceresponse = await client.PostAsJsonAsync("api/ReportState/GetOne", new RaModels.RequestModel<RaModels.ReportQueryInfo>()))
            {
                var result = new ResponseModel<ReportQueryInfo>();
                if (serviceresponse.IsSuccessStatusCode)
                {
                    result = await serviceresponse.Content.ReadFromJsonAsync<ResponseModel<ReportQueryInfo>>();
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
            using (HttpResponseMessage serviceresponse = await client.PostAsJsonAsync("api/ReportState/GetList", new RaModels.RequestModel<RaModels.ReportQueryInfo>()))
            {
                var result = new ResponseModel<ReportQueryInfo>();
                if (serviceresponse.IsSuccessStatusCode)
                {
                    result = await serviceresponse.Content.ReadFromJsonAsync<ResponseModel<ReportQueryInfo>>();
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

            var queryInfo = new RaModels.ReportQueryInfo() { UUID = "002", ReportState = "S1", RequestDate = DateTime.Now };

            using (HttpResponseMessage serviceresponse = await client.PostAsJsonAsync("api/ReportState/Create", new RaModels.RequestModel<RaModels.ReportQueryInfo>() { Item = queryInfo }))
            {
                var result = new ResponseModel<ReportQueryInfo>();
                if (serviceresponse.IsSuccessStatusCode)
                {
                    result = await serviceresponse.Content.ReadFromJsonAsync<ResponseModel<ReportQueryInfo>>();
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

            var queryInfo = new RaModels.ReportQueryInfo() { UUID = "002", ReportState = "S1", RequestDate = DateTime.Now };

            using (HttpResponseMessage serviceresponse = await client.PostAsJsonAsync("api/ReportState/Create", new RaModels.RequestModel<RaModels.ReportQueryInfo>() { Item = queryInfo, RecordId = "002" }))
            {
                var result = new ResponseModel<ReportQueryInfo>();
                if (serviceresponse.IsSuccessStatusCode)
                {
                    result = await serviceresponse.Content.ReadFromJsonAsync<ResponseModel<ReportQueryInfo>>();
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