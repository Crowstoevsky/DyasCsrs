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
            vm.StockDisponible = await _context.StockSucursales
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
                _context.Clientes.Add(vm.Cliente);
                await _context.SaveChangesAsync();
                vm.ClienteId = vm.Cliente.Id;
                return View(vm);
            }

            if (accion == "agregarDetalle")
            {
                vm.Detalles ??= new List<DetallesVenta>();

                var stock = await _context.StockSucursales
                    .Include(s => s.ProductoMoto)
                    .Include(s => s.Sucursal)
                    .FirstOrDefaultAsync(s => s.Id == vm.StockSucursalId);

                if (stock != null)
                {
                    var detalle = new DetallesVenta
                    {
                        ProductoMoto = stock.ProductoMoto,
                        Cantidad = vm.DetallesTempCantidad,
                        PrecioUnitario = stock.ProductoMoto.Precio,
                        SubTotal = stock.ProductoMoto.Precio * vm.DetallesTempCantidad,

                        // NUEVO: Guardamos de qué stock viene
                        // Puedes agregar esto como propiedad temporal en tu DetallesVenta si no existe
                        // StockSucursalId = stock.Id
                    };

                    // Aquí debes permitir duplicados si son de distinta sucursal.
                    vm.Detalles.Add(detalle);
                }

                return View(vm);
            }


            if (accion == "registrarVenta")
            {
                var empleadoIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(empleadoIdStr, out int empleadoId))
                    return Unauthorized();

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
                    if (detalleInput.ProductoMoto?.Id == 0 || detalleInput.Cantidad <= 0)
                        continue;

                    var producto = await _context.ProductoMotos.FindAsync(detalleInput.ProductoMoto.Id);
                    if (producto == null) continue;

                    var subtotal = producto.Precio * detalleInput.Cantidad;

                    venta.Detalles.Add(new DetallesVenta
                    {
                        ProductoMoto = producto,
                        Cantidad = detalleInput.Cantidad,
                        PrecioUnitario = producto.Precio,
                        SubTotal = subtotal
                    });

                    venta.Total += subtotal;

                    // Descontar stock de la sucursal correcta
                    var stock = await _context.StockSucursales
                        .FirstOrDefaultAsync(s => s.ProductoMotoId == producto.Id && s.Cantidad >= detalleInput.Cantidad);
                    if (stock != null)
                        stock.Cantidad -= detalleInput.Cantidad;
                }

                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(vm);
        }
    }
}
