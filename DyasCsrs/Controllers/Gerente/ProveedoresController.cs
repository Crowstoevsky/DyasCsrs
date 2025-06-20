using DyasCsrs.Data;
using DyasCsrs.Models;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DyasCsrs.Controllers.Gerente
{
    public class ProveedoresController : Controller
    {
        private readonly AppDbContext _appDbcontext;

        public ProveedoresController(AppDbContext context)
        {
            _appDbcontext = context;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new ProveedoresCrudVM
            {
                Proveedores = _appDbcontext.Proveedores.ToList(),
                Proveedor = new Proveedor(),
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(ProveedoresCrudVM model)
        {
            Proveedor nuevoProveedor = new Proveedor
            {
                Nombre = model.Proveedor.Nombre,
                Telefono = model.Proveedor.Telefono,
                Email = model.Proveedor.Email
            };

            await _appDbcontext.Proveedores.AddAsync(nuevoProveedor);
            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProveedoresCrudVM model)
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
