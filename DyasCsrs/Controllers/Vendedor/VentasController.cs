using Microsoft.AspNetCore.Mvc;

namespace DyasCsrs.Controllers.Vendedor
{
    public class VentasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
