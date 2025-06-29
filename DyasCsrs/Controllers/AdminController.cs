using System.Globalization;
using DyasCsrs.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DyasCsrs.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalEmpleados = await _context.Empleados.CountAsync();

            // Ventas del mes actual (usando DateOnly)
            var hoy = DateOnly.FromDateTime(DateTime.Today);
            ViewBag.VentasMes = await _context.Ventas
                .Where(v => v.Fecha.Month == hoy.Month && v.Fecha.Year == hoy.Year)
                .SumAsync(v => (decimal?)v.Total) ?? 0;

            ViewBag.TotalDevoluciones = await _context.OpcionesDevolucion.CountAsync();

            // Ventas agrupadas por mes (DateOnly requiere AsEnumerable para usar Month/Year)
            var ventas = _context.Ventas
                .AsEnumerable() // Traer todo a memoria para usar DateOnly
                .GroupBy(v => new { v.Fecha.Year, v.Fecha.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new
                {
                    Mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                    Total = g.Sum(v => v.Total)
                })
                .ToList();

            ViewBag.Meses = ventas.Select(v => v.Mes).ToList();
            ViewBag.VentasPorMes = ventas.Select(v => v.Total).ToList();

            // Productos más vendidos
            var topProductos = await _context.DetallesVentas
                .Include(d => d.ProductoMoto)
                .GroupBy(d => d.ProductoMoto.Marca + " " + d.ProductoMoto.Modelo)
                .Select(g => new
                {
                    Nombre = g.Key,
                    Cantidad = g.Sum(d => d.Cantidad)
                })
                .OrderByDescending(g => g.Cantidad)
                .Take(5)
                .ToListAsync();

            ViewBag.ProductosTop = topProductos.Select(p => p.Nombre).ToList();
            ViewBag.CantidadesVendidas = topProductos.Select(p => p.Cantidad).ToList();

            return View();
        }



    }
}
