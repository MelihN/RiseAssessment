using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RAUI.Controllers.Tests
{
    [TestClass()]
    public class PersonReportControllerTests
    {
        IConfiguration _configuration;
        public PersonReportControllerTests(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [TestMethod()]
        public async Task PersonReportDtoDataTestAsync()
        {
            var controller = new PersonReportController(_configuration);

            var result = await controller.PersonReportDtoData(new Models.DtoModel());
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}