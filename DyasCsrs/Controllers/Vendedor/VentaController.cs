using System.Security.Claims;
using DyasCsrs.Data;
using DyasCsrs.Models;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DyasCsrs.Controllers.Vendedor
{
    public class VentaController : Controller
    {
        private readonly AppDbContext _context;

        public VentaController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    int vendedorId = ObtenerEmpleadoId(); // Ajusta esto según tu autenticación

        //    var vm = new VentaVM
        //    {
        //        HistorialVentas = await _context.Ventas
        //            .Include(v => v.Cliente)
        //            .Include(v => v.Detalles).ThenInclude(d => d.ProductoMoto)
        //            .Where(v => v.Empleado.Id == vendedorId)
        //            .OrderByDescending(v => v.Fecha)
        //            .ToListAsync(),

        //        ProductosMotos = await _context.ProductoMotos.ToListAsync(),
        //        Sucursales = await _context.Sucursales.ToListAsync(),
        //        MetodosPago = await _context.MetodosPago.ToListAsync()
        //    };

        //    return View(vm);
        //}

        //[HttpPost]
        //public async Task<IActionResult> RegistrarVenta(VentaVM model)
        //{
        //    if (!ModelState.IsValid || model.Productos.Count == 0)
        //    {
        //        TempData["Error"] = "Datos inválidos. Verifica todos los campos.";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    // Buscar cliente por DNI
        //    var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.DNI == model.DNICliente);
        //    if (cliente == null)
        //    {
        //        cliente = new Cliente
        //        {
        //            DNI = model.DNICliente,
        //            Nombre = "Nuevo Cliente",
        //            Telefono = "000000000",
        //            Direccion = "Sin Dirección"
        //        };
        //        _context.Clientes.Add(cliente);
        //        await _context.SaveChangesAsync();
        //    }

        //    // Crear nueva venta
        //    var venta = new Venta
        //    {
        //        Cliente = cliente,
        //        Empleado = await _context.Empleados.FindAsync(ObtenerEmpleadoId()),
        //        MetodoPago = await _context.MetodosPago.FirstAsync(), // Cambiar si tienes un dropdown
        //        Total = 0,
        //        Fecha = DateOnly.FromDateTime(DateTime.Now),
        //        Detalles = new List<DetallesVenta>()
        //    };

        //    foreach (var item in model.Productos)
        //    {
        //        var producto = await _context.ProductoMotos.FindAsync(item.ProductoMotoId);
        //        var stock = await _context.StockSucursales
        //            .FirstOrDefaultAsync(s => s.ProductoMotoId == item.ProductoMotoId && s.SucursalId == item.SucursalId);

        //        if (stock == null || stock.Cantidad < item.Cantidad)
        //        {
        //            TempData["Error"] = $"Stock insuficiente para {producto.Marca} {producto.Modelo}";
        //            return RedirectToAction(nameof(Index));
        //        }

        //        stock.Cantidad -= item.Cantidad;

        //        var detalle = new DetallesVenta
        //        {
        //            ProductoMoto = producto,
        //            Cantidad = item.Cantidad,
        //            PrecioUnitario = producto.Precio,
        //            SubTotal = producto.Precio * item.Cantidad
        //        };

        //        venta.Detalles.Add(detalle);
        //        venta.Total += detalle.SubTotal;
        //    }

        //    _context.Ventas.Add(venta);
        //    await _context.SaveChangesAsync();

        //    TempData["Success"] = "Venta registrada correctamente.";
        //    return RedirectToAction(nameof(Index));
        //}

        //// Método auxiliar (ajustar según tu sistema de login)
        //private int ObtenerEmpleadoId()
        //{
        //    var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    if (claim == null)
        //        throw new Exception("No se pudo obtener el ID del empleado desde los claims.");

        //    return int.Parse(claim.Value);
        //}

    }
}
