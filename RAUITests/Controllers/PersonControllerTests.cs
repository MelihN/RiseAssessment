using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RAUI.Controllers.Tests
{
    [TestClass()]
    public class PersonControllerTests
    {
        IConfiguration _configuration;
        public PersonControllerTests(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [TestMethod()]
        public async Task PersonDtoDataTestAsync()
        {
            var controller = new PersonController(_configuration);

            var result = await controller.PersonDtoData(new Models.DtoModel());
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod()]
        public async Task PersonCreateTestAsync()
        {
            var controller = new PersonController(_configuration);
            var person = new RaModels.Person()
            {
                UUID = "01",
                Name = "Name1",
                Surname = "Surname1",
                CompanyName = "CompanyName1",
                ContactInfos = new List<RaModels.ContactInfo>() {
                     new RaModels.ContactInfo(){ Location="Ankara", Email="name1@rise.com", PhoneNumber="903123212121"}
                    }
            };
            var result = await controller.PersonCreate(person);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod()]
        public async Task PersonDeleteTestAsync()
        {
            var controller = new PersonController(_configuration);

            string id = "123";
            var result = await controller.PersonDelete(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}