using DyasCsrs.Data;
using DyasCsrs.Models;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DyasCsrs.Controllers
{
    [Authorize(Roles = "Vendedor")]
    public class VentaController : Controller
    {
        private readonly AppDbContext _context;

        public VentaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new VentaVM
            {
                StockDisponible = await _context.StockSucursales
                    .Where(s => s.Cantidad > 0) 
                    .Include(s => s.ProductoMoto)
                    .Include(s => s.Sucursal)
                    .ToListAsync(),

                MetodosPago = await _context.MetodosPago.ToListAsync(),
                Clientes = await _context.Clientes.ToListAsync(),
                Ventas = await _context.Ventas
                    .Include(v => v.Cliente)
                    .Include(v => v.Detalles).ThenInclude(d => d.ProductoMoto)
                    .ToListAsync(),
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(VentaVM vm, string accion)
        {
            Console.WriteLine($"--- Acción recibida: {accion}");

            vm.StockDisponible = await _context.StockSucursales
                .Where(s => s.Cantidad > 0)
                .Include(s => s.ProductoMoto)
                .Include(s => s.Sucursal)
                .ToListAsync();

            vm.MetodosPago = await _context.MetodosPago.ToListAsync();
            vm.Clientes = await _context.Clientes.ToListAsync();
            vm.Ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles).ThenInclude(d => d.ProductoMoto)
                .ToListAsync();

            if (accion == "buscar")
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.DNI == vm.Cliente.DNI);
                Console.WriteLine(cliente != null ? $"Cliente encontrado: {cliente.Nombre}" : "Cliente no encontrado.");
                if (cliente != null)
                {
                    vm.Cliente = cliente;
                    vm.ClienteId = cliente.Id;
                }
                else
                {
                    ModelState.AddModelError("", "Cliente no encontrado. Complete los datos para crearlo.");
                }
                return View(vm);
            }

            if (accion == "crearCliente")
            {
                Console.WriteLine($"Creando nuevo cliente: {vm.Cliente.Nombre}, DNI: {vm.Cliente.DNI}");
                _context.Clientes.Add(vm.Cliente);
                await _context.SaveChangesAsync();
                vm.ClienteId = vm.Cliente.Id;
                return View(vm);
            }

            if (accion == "agregarDetalle")
            {
                Console.WriteLine("Agregando detalle...");

                vm.Detalles ??= new List<DetallesVenta>();

                var stock = await _context.StockSucursales
                    .Where(s => s.Cantidad > 0)
                    .Include(s => s.ProductoMoto)
                    .Include(s => s.Sucursal)
                    .FirstOrDefaultAsync(s => s.Id == vm.StockSucursalId);

                if (stock != null)
                {
                    Console.WriteLine($"Stock encontrado: {stock.ProductoMoto.Marca} {stock.ProductoMoto.Modelo} - Cantidad disponible: {stock.Cantidad}");

                    var yaAgregados = vm.Detalles
                        .Where(d => d.StockSucursalId == stock.Id)
                        .Sum(d => d.Cantidad);

                    if (vm.DetallesTempCantidad + yaAgregados > stock.Cantidad)
                    {
                        ViewData["ErrorStock"] = $"Stock insuficiente. Ya agregaste {yaAgregados} y quedan {stock.Cantidad} unidades.";
                        return View(vm);
                    }

                    var detalle = new DetallesVenta
                    {
                        ProductoMoto = stock.ProductoMoto,
                        Cantidad = vm.DetallesTempCantidad,
                        PrecioUnitario = stock.ProductoMoto.Precio,
                        SubTotal = stock.ProductoMoto.Precio * vm.DetallesTempCantidad,
                        StockSucursalId = stock.Id
                    };

                    Console.WriteLine($"Agregado: {detalle.ProductoMoto.Marca} x {detalle.Cantidad} - SubTotal: {detalle.SubTotal}");

                    vm.Detalles.Add(detalle);
                }

                foreach (var d in vm.Detalles)
                {
                    d.ProductoMoto = await _context.ProductoMotos.FindAsync(d.ProductoMoto?.Id);
                }

                return View(vm);
            }

            if (accion == "registrarVenta")
            {
                Console.WriteLine("Registrando venta...");

                if (vm.Detalles == null || !vm.Detalles.Any())
                {
                    Console.WriteLine("No hay detalles para registrar.");
                    ViewData["ErrorStock"] = "Debe agregar al menos un producto.";
                    return View(vm);
                }

                var empleadoIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(empleadoIdStr, out int empleadoId))
                {
                    Console.WriteLine("Empleado no identificado.");
                    return Unauthorized();
                }

                var venta = new Venta
                {
                    ClienteId = vm.ClienteId,
                    EmpleadoId = empleadoId,
                    MetodoPagoId = vm.MetodoPagoId,
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    Detalles = new List<DetallesVenta>(),
                    Total = 0
                };

                foreach (var detalleInput in vm.Detalles)
                {
                    Console.WriteLine($"Procesando detalle: ProductoID = {detalleInput.ProductoMoto?.Id}, Cantidad = {detalleInput.Cantidad}");

                    if (detalleInput.ProductoMoto?.Id == 0 || detalleInput.Cantidad <= 0)
                        continue;

                    var producto = await _context.ProductoMotos.FindAsync(detalleInput.ProductoMoto.Id);
                    var stock = await _context.StockSucursales.FirstOrDefaultAsync(s => s.Id == detalleInput.StockSucursalId);

                    if (producto == null || stock == null)
                    {
                        Console.WriteLine("Producto o stock no encontrado.");
                        ViewData["ErrorStock"] = "Error: uno de los productos ya no tiene suficiente stock.";
                        return View(vm);
                    }

                    if (stock.Cantidad < detalleInput.Cantidad)
                    {
                        Console.WriteLine($"Stock insuficiente para el producto ID {producto.Id}");
                        ViewData["ErrorStock"] = "Stock insuficiente al registrar venta.";
                        return View(vm);
                    }

                    var subtotal = producto.Precio * detalleInput.Cantidad;

                    var detalle = new DetallesVenta
                    {
                        ProductoMoto = producto,
                        ProductoMotoID = producto.Id,
                        Cantidad = detalleInput.Cantidad,
                        PrecioUnitario = producto.Precio,
                        SubTotal = subtotal,
                        StockSucursalId = detalleInput.StockSucursalId
                    };

                    venta.Detalles.Add(detalle);
                    venta.Total += subtotal;

                    stock.Cantidad -= detalleInput.Cantidad;

                    Console.WriteLine($"Detalle agregado: {producto.Marca} {producto.Modelo}, Subtotal: {subtotal}");
                }

                Console.WriteLine($"Venta total: {venta.Total}");

                try
                {
                    _context.Ventas.Add(venta);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Venta guardada correctamente en la base de datos.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar venta: {ex.Message}");
                    ViewData["Error"] = $"Error al guardar la venta: {ex.Message}";
                    return View(vm);
                }

                return RedirectToAction("Index");
            }

            return View(vm);
        }



    }
}
