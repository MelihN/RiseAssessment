using Microsoft.AspNetCore.Mvc;
using RAUI.BLL;
using RAUI.Models;
using System.Text.Json;

namespace RAUI.Controllers
{
    public class PersonReportController : Controller
    {
        private readonly string? baseUrl;

        public PersonReportController(IConfiguration configuration)
        {
            baseUrl = configuration.GetValue<string>("baseUrl");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PersonReportDtoData(DtoModel gridRequest)
        {
            var bll = new PersonReportBLL(baseUrl);
            var response = await bll.GetReportData(gridRequest);
            return Json(response, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
