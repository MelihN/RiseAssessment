using Microsoft.AspNetCore.Mvc;

namespace RAUI.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
