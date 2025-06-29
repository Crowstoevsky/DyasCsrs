using DyasCsrs.Data;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DyasCsrs.Controllers.Vendedor
{
    [Authorize(Roles = "Vendedor")]
    public class HistorialController : Controller
    {
        private readonly AppDbContext _appDbcontext;
        
        public HistorialController(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var vm = new HistorialVM
            {
                Ventas = _appDbcontext.Ventas.Include(v => v.Cliente)
                                              .Include(v => v.Detalles)
                                              .ThenInclude(d => d.ProductoMoto)
                                              .ToList()
            };
            return View(vm);
        }
    }
}
