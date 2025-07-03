using DyasCsrs.Data;
using DyasCsrs.Models;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DyasCsrs.Controllers.Gerente
{
    [Authorize(Roles = "Gerente")]
    public class ProductosController : Controller
    {
        private readonly AppDbContext _appDbcontext;

        public ProductosController(AppDbContext context)
        {
            _appDbcontext = context;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new ProductoMotoCrudVM
            {
                ProductosMotos = _appDbcontext.ProductoMotos.ToList(),
                ProductoMoto = new ProductoMoto(),
                Proveedores = _appDbcontext.Proveedores.ToList(),
                EstadosProductoMotos = _appDbcontext.EstadosProductoMotos.ToList(),
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(ProductoMotoCrudVM model)
        {
            ProductoMoto nuevoProductoMoto = new ProductoMoto
            {
                Marca = model.ProductoMoto.Marca,
                Modelo = model.ProductoMoto.Modelo,
                CC = model.ProductoMoto.CC,
                Anio = model.ProductoMoto.Anio,
                Color = model.ProductoMoto.Anio,
                Precio = model.ProductoMoto.Precio,
                ProveedorId = model.ProductoMoto.ProveedorId,
                EstadoPMId = 1,

            };

            await _appDbcontext.ProductoMotos.AddAsync(nuevoProductoMoto);
            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProductoMotoCrudVM model)
        {


            var productoExistente = await _appDbcontext.ProductoMotos.FindAsync(model.ProductoMotoId);
            if (productoExistente == null)
                return RedirectToAction(nameof(Index));

            productoExistente.Marca = model.ProductoMoto.Marca;
            productoExistente.Modelo = model.ProductoMoto.Modelo;
            productoExistente.CC = model.ProductoMoto.CC;
            productoExistente.Anio = model.ProductoMoto.Anio;
            productoExistente.Color = model.ProductoMoto.Anio;
            productoExistente.Precio = model.ProductoMoto.Precio;
            productoExistente.ProveedorId = model.ProductoMoto.ProveedorId;
            productoExistente.EstadoPMId = model.ProductoMoto.EstadoPMId;


            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int idProductoMoto)
        {
            var productoMoto = await _appDbcontext.ProductoMotos.FindAsync(idProductoMoto);
            if (productoMoto != null)
            {
                productoMoto.EstadoPMId = 2; // Inactivo
                await _appDbcontext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Activar(int idProductoMoto)
        {
            var producto = await _appDbcontext.ProductoMotos.FindAsync(idProductoMoto);
            if (producto != null && producto.EstadoPMId == 2) // solo si estaba inactivo
            {
                producto.EstadoPMId = 1; // Activo
                await _appDbcontext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
