﻿using DyasCsrs.Data;
using DyasCsrs.Models;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DyasCsrs.Controllers.Gerente
{
    [Authorize(Roles = "Gerente")]
    public class ProveedoresController : Controller
    {
        private readonly AppDbContext _appDbcontext;

        public ProveedoresController(AppDbContext context)
        {
            _appDbcontext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new ProveedoresCrudVM
            {
                Proveedores = _appDbcontext.Proveedores.ToList(),
                Proveedor = new Proveedor(),
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(ProveedoresCrudVM model)
        {
            Proveedor nuevoProveedor = new Proveedor
            {
                Nombre = model.Proveedor.Nombre,
                Telefono = model.Proveedor.Telefono,
                Email = model.Proveedor.Email
            };

            await _appDbcontext.Proveedores.AddAsync(nuevoProveedor);
            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProveedoresCrudVM model)
        {

            var proveedorExistente = await _appDbcontext.Proveedores.FindAsync(model.ProveedorId);
            if (proveedorExistente == null)
                return RedirectToAction(nameof(Index));

            proveedorExistente.Nombre = model.Proveedor.Nombre;
            proveedorExistente.Telefono = model.Proveedor.Telefono;
            proveedorExistente.Email = model.Proveedor.Email;


            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int idProveedor)
        {
            var proveedor = await _appDbcontext.Proveedores.FindAsync(idProveedor);
            if (proveedor != null)
            {
                _appDbcontext.Proveedores.Remove(proveedor);
                await _appDbcontext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
