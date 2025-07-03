using DyasCsrs.Data;
using DyasCsrs.Models;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DyasCsrs.Controllers.Gerente
{
    [Authorize(Roles = "Gerente")]
    public class StockController : Controller
    {
        private readonly AppDbContext _appDbcontext;

        public StockController(AppDbContext context)
        {
            _appDbcontext = context;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new StockCrudVM
            {
                Stocks = _appDbcontext.StockSucursales
                    .Include(s => s.ProductoMoto)
                    .Include(s => s.Sucursal)
                    .ToList(),

                Stock = new StockSucursal(),
                ProductosMotos = _appDbcontext.ProductoMotos
                    .Where(pm => pm.EstadoPMId == 1) // solo productos activos
                    .ToList(),

                Sucursales = _appDbcontext.Sucursales
                    .Where(s => s.Activo)
                    .ToList(),

            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(StockCrudVM model)
        {
            if (model.Stock.Cantidad <= 0)
            {
                ViewData["ErrorStock"] = "La cantidad debe ser mayor a 0.";
                return View(model);
            }
            StockSucursal nuevoStock = new StockSucursal
            {
                Cantidad = model.Stock.Cantidad,
                ProductoMotoId = model.Stock.ProductoMotoId,
                SucursalId = model.Stock.SucursalId

            };

            await _appDbcontext.StockSucursales.AddAsync(nuevoStock);
            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Editar(StockCrudVM model)
        {
            if (model.Stock.Cantidad <= 0)
            {
                ViewData["ErrorStock"] = "La cantidad debe ser mayor a 0.";
                return View(model);
            }
            var stockExistente = await _appDbcontext.StockSucursales.FindAsync(model.StockId);
            if (stockExistente == null)
                return RedirectToAction(nameof(Index));

            stockExistente.Cantidad = model.Stock.Cantidad;
            stockExistente.ProductoMotoId = model.Stock.ProductoMotoId;
            stockExistente.SucursalId = model.Stock.SucursalId;

            await _appDbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Eliminar(int idStock)
        {
            var stock = await _appDbcontext.StockSucursales.FindAsync(idStock);
            if (stock != null)
            {
                _appDbcontext.StockSucursales.Remove(stock);
                await _appDbcontext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
