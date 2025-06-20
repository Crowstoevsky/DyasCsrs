using Microsoft.AspNetCore.Mvc;

namespace DyasCsrs.Controllers.Vendedor
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
