using Microsoft.AspNetCore.Mvc;

namespace DyasCsrs.Controllers.Gerente
{
    public class StockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
