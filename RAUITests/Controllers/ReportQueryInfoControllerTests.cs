using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaModels;
using RAUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RAUI.Controllers.Tests
{
    [TestClass()]
    public class ReportQueryInfoControllerTests
    {
        IConfiguration _configuration;
        public ReportQueryInfoControllerTests(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [TestMethod()]
        public async void ReportQueryInfoDtoDataTest()
        {
            var controller = new ReportQueryInfoController(_configuration);
                     
            var result = await controller.ReportQueryInfoDtoData(new Models.DtoModel());
            var okResult = result as OkObjectResult;
                        
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);            
        }

        [TestMethod()]
        public async Task ReportQueryInfoDeleteTestAsync()
        {
            var controller = new ReportQueryInfoController(_configuration);
            string id = "123";
            var result = await controller.ReportQueryInfoDelete(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}