using DyasCsrs.Data;
using DyasCsrs.Models;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                Stocks = _appDbcontext.StockSucursales.ToList(),
                Stock = new StockSucursal(),
                ProductosMotos = _appDbcontext.ProductoMotos.ToList(),
                Sucursales = _appDbcontext.Sucursales.ToList(),
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(StockCrudVM model)
        {
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
        [HttpPost]
        public async Task<IActionResult> Editar(StockCrudVM model)
        {
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
