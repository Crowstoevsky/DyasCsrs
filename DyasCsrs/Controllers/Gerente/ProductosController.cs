using Microsoft.AspNetCore.Mvc;

namespace DyasCsrs.Controllers.Gerente
{
    public class ProductosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
