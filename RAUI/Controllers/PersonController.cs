using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RaModels;
using RAUI.BLL;
using RAUI.Models;
using System.Text.Json;

namespace RAUI.Controllers
{
    public class PersonController : Controller
    {
        private readonly string baseUrl;

        public PersonController(IConfiguration configuration)
        {            
            baseUrl = configuration.GetValue<string>("baseUrl"); 
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PersonDtoData(DtoModel gridRequest)
        {
            var bll = new PersonBLL(baseUrl);
            var response = await bll.GetList(gridRequest);
            return Json(response, new JsonSerializerOptions { WriteIndented = true });
        }


        [HttpPost]
        public async Task<IActionResult> PersonCreate(Person model)
        {            
            var bll = new PersonBLL(baseUrl);            
            var response = await bll.Create(new RequestModel<Person> { Item = model } );            
            return Json(response, new JsonSerializerOptions { WriteIndented = true });
        }

        [HttpPost]
        public async Task<IActionResult> PersonDelete(string Id)
        {            
            var bll = new PersonBLL(baseUrl);
            var response = await bll.Delete(new RequestModel<Person>() { RecordId = Id});
            return Json(response, new JsonSerializerOptions { WriteIndented = true });
        }

    }
}
