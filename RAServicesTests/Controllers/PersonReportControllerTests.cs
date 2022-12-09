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
    public class PersonReportControllerTests
    {
        [TestMethod()]
        public async void GetReportDataTest()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:49155");
            client.Timeout = TimeSpan.FromMinutes(5);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage serviceresponse = await client.PostAsJsonAsync("api/PersonReport/GetList", new RaModels.RequestModel<RaModels.PersonReport>()))
            {
                var result = new ResponseModel<PersonReport>();
                if (serviceresponse.IsSuccessStatusCode)
                {
                    result = await serviceresponse.Content.ReadFromJsonAsync<ResponseModel<PersonReport>>();
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