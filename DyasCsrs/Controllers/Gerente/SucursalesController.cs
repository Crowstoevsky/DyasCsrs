using DyasCsrs.Data;
using DyasCsrs.Models;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DyasCsrs.Controllers.Gerente
{
    [Authorize(Roles = "Gerente")]
    public class SucursalesController : Controller
    {
        private readonly AppDbContext _appDbcontext;

        public SucursalesController(AppDbContext context)
        {
            _appDbcontext = context;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new SucursalesCrudVM
            {
                Sucursales = _appDbcontext.Sucursales.ToList(),
                Sucursal = new Sucursal(),
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(SucursalesCrudVM model)
        {
            Sucursal nuevaSucursal = new Sucursal
            {
                Nombre = model.Sucursal.Nombre,
                Ubicacion = model.Sucursal.Ubicacion,
                Activo = true 
            };

            await _appDbcontext.Sucursales.AddAsync(nuevaSucursal);
            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Editar(SucursalesCrudVM model)
        {


            var sucursalExistente = await _appDbcontext.Sucursales.FindAsync(model.SucursalId);
            if (sucursalExistente == null)
                return RedirectToAction(nameof(Index));

            sucursalExistente.Nombre = model.Sucursal.Nombre;
            sucursalExistente.Ubicacion = model.Sucursal.Ubicacion;


            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int idSucursal)
        {
            var sucursal = await _appDbcontext.Sucursales.FindAsync(idSucursal);
            if (sucursal == null) return RedirectToAction(nameof(Index));

            sucursal.Activo = false;
            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Activar(int idSucursal)
        {
            var sucursal = await _appDbcontext.Sucursales.FindAsync(idSucursal);
            if (sucursal != null && !sucursal.Activo)
            {
                sucursal.Activo = true;
                await _appDbcontext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

    }
}
