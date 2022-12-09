using Microsoft.AspNetCore.Mvc;
using RaModels;
using RAUI.BLL;
using RAUI.Models;
using System.Text.Json;

namespace RAUI.Controllers
{
    public class ReportQueryInfoController : Controller
    {
        private readonly string? baseUrl;        
        public ReportQueryInfoController(IConfiguration configuration)
        {
            baseUrl = configuration.GetValue<string>("baseUrl");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReportQueryInfoDtoData(DtoModel gridRequest)
        {
            var bll = new ReportQueryInfoBLL(baseUrl);
            var response = await bll.GetList(gridRequest);
            return Json(response, new JsonSerializerOptions { WriteIndented = true });
        }


        [HttpPost]
        public async Task<IActionResult> ReportQueryInfoDelete(string Id)
        {
            var bll = new ReportQueryInfoBLL(baseUrl);
            var response = await bll.Delete(new RequestModel<ReportQueryInfo>() { RecordId = Id });
            return Json(response, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
