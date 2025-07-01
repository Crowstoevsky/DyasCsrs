using DyasCsrs.Data;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DyasCsrs.Controllers.Gerente
{
    [Authorize(Roles = "Gerente")]
    public class DevolucionesController : Controller
    {
        private readonly AppDbContext _context;

        public DevolucionesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var devoluciones = _context.OpcionesDevolucion
                .Include(o => o.Venta)
                    .ThenInclude(v => v.Cliente)
                .Include(o => o.EstadoDevolucion)
                .Include(o => o.Venta.Detalles)
                    .ThenInclude(d => d.ProductoMoto)
                .Where(o => o.EstadoDId == 1) // Estado "Pendiente"
                .ToList();

            var vm = new PanelDevolucionesVM
            {
                DevolucionesPendientes = _context.OpcionesDevolucion
                .Where(d => d.EstadoDevolucion.Nombre == "Pendiente")
                .Include(d => d.Venta).ThenInclude(v => v.Cliente)
                .Include(d => d.Venta).ThenInclude(v => v.Detalles).ThenInclude(p => p.ProductoMoto)
                .Include(d => d.EstadoDevolucion)
                .ToList(),

                        HistorialDevoluciones = _context.OpcionesDevolucion
                .Where(d => d.EstadoDevolucion.Nombre != "Pendiente")
                .Include(d => d.Venta).ThenInclude(v => v.Cliente)
                .Include(d => d.EstadoDevolucion)
                .ToList()
            };


            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Aprobar(int id)
        {
            var devolucion = await _context.OpcionesDevolucion
                .Include(d => d.Venta)
                    .ThenInclude(v => v.Detalles)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (devolucion == null)
                return RedirectToAction("Index");

            devolucion.EstadoDId = 2; // Aprobado

            var venta = devolucion.Venta;

            if (venta != null)
            {
                // Poner el total de la venta a 0 para que no se cuente en el dashboard
                venta.Total = 0.00m;

                foreach (var detalle in venta.Detalles)
                {
                    var productoId = detalle.ProductoMotoID;
                    var stockSucursalId = detalle.StockSucursalId;

                    // Validación básica
                    if (stockSucursalId <= 0)
                        continue;

                    // Buscar el stock real en la tabla StockSucursales
                    var stockReal = await _context.StockSucursales
                        .FirstOrDefaultAsync(s => s.Id == stockSucursalId && s.ProductoMotoId == productoId);

                    if (stockReal != null)
                    {
                        stockReal.Cantidad += detalle.Cantidad;
                    }
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Rechazar(int id)
        {
            var devolucion = await _context.OpcionesDevolucion.FindAsync(id);
            if (devolucion == null) return RedirectToAction("Index");

            devolucion.EstadoDId = 3; // Rechazado
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
