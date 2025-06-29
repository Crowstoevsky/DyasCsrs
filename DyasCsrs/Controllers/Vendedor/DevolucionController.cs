using DyasCsrs.Data;
using DyasCsrs.Models;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DyasCsrs.Controllers
{
    [Authorize(Roles = "Vendedor")]
    public class DevolucionController : Controller
    {
        private readonly AppDbContext _context;

        public DevolucionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new DevolucionVM());
        }

        [HttpPost]
        public async Task<IActionResult> BuscarCliente(DevolucionVM vm)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.DNI == vm.DNICliente);

            if (cliente == null)
            {
                vm.MensajeError = "Cliente no encontrado.";
                return View("Index", vm);
            }

            var ventas = await _context.Ventas
                .Include(v => v.Detalles).ThenInclude(d => d.ProductoMoto)
                .Include(v => v.OpcionDevolucion) // Necesario para filtrar
                .Where(v => v.ClienteId == cliente.Id && v.OpcionDevolucion == null)
                .ToListAsync();

            vm.ClienteEncontrado = cliente;
            vm.VentasCliente = ventas;

            return View("Index", vm);
        }


        [HttpPost]
        public async Task<IActionResult> SolicitarDevolucion(DevolucionVM vm)
        {
            vm.NuevaDevolucion.FechaSolicitud = DateOnly.FromDateTime(DateTime.Now);
            vm.NuevaDevolucion.EstadoDId = 1; // Pendiente
            _context.OpcionesDevolucion.Add(vm.NuevaDevolucion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
