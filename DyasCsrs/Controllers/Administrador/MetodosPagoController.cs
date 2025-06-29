using DyasCsrs.Data;
using DyasCsrs.Models;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DyasCsrs.Controllers.Administrador
{
    [Authorize(Roles = "Administrador")]
    public class MetodosPagoController : Controller
    {
        private readonly AppDbContext _appDbcontext;

        public MetodosPagoController(AppDbContext context)
        {
            _appDbcontext = context;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new MetodosPagoCrudVM
            {
                MetodosPago = _appDbcontext.MetodosPago.ToList(),
                MetodoPago = new MetodoPago() // vacío para formulario
            };
            
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(MetodosPagoCrudVM model)
        {
            MetodoPago nuevoMetodoPago = new MetodoPago
            {
                Nombre = model.MetodoPago.Nombre
            };
            
            await _appDbcontext.MetodosPago.AddAsync(nuevoMetodoPago);
            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Editar(MetodosPagoCrudVM model)
        {


            var metodoExistente = await _appDbcontext.MetodosPago.FindAsync(model.MetodoPagoId);
            if (metodoExistente == null)
                return RedirectToAction(nameof(Index));

            metodoExistente.Nombre = model.MetodoPago.Nombre;

            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int idMetodoPago)
        {
            var metodoPago = await _appDbcontext.MetodosPago.FindAsync(idMetodoPago);
            if (metodoPago != null)
            {
                _appDbcontext.MetodosPago.Remove(metodoPago);
                await _appDbcontext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
