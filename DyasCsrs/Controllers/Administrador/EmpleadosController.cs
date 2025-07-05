using DyasCsrs.Data;
using DyasCsrs.ViewModels;
using DyasCsrs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DyasCsrs.Controllers.Administrador
{
    [Authorize(Roles = "Administrador")]
    public class EmpleadosController : Controller
    {
        private readonly AppDbContext _appDbcontext;

        public EmpleadosController(AppDbContext context)
        {
            _appDbcontext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var empleadoCrud = new EmpleadoCrudVM
            {
                Empleados = await _appDbcontext.Empleados.Include(e => e.Rol).ToListAsync(),
                Roles = await _appDbcontext.Roles.ToListAsync(),
                Empleado = new Empleado() 
            };
            return View(empleadoCrud);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(EmpleadoCrudVM model)
        {
            var emailExiste = await _appDbcontext.Empleados
                .AnyAsync(e => e.Email == model.Empleado.Email);

            var rolExiste = await _appDbcontext.Roles
                .AnyAsync(r => r.Id == model.Empleado.RolId);

            if (emailExiste || !rolExiste)
            {
                if (emailExiste)
                {
                    ModelState.AddModelError("Empleado.Email", "El correo ya está en uso.");
                }

                if (!rolExiste)
                {
                    ModelState.AddModelError("Empleado.RolId", "Debe seleccionar un rol válido.");
                }

                model.Empleados = await _appDbcontext.Empleados.Include(e => e.Rol).ToListAsync();
                model.Roles = await _appDbcontext.Roles.ToListAsync();

                model.EmpleadoId = 0; 
                return View("Index", model);
            }

            var nuevoEmpleado = new Empleado
            {
                Nombre = model.Empleado.Nombre,
                Email = model.Empleado.Email,
                Telefono = model.Empleado.Telefono,
                Password = model.Empleado.Password,
                RolId = model.Empleado.RolId
            };

            await _appDbcontext.Empleados.AddAsync(nuevoEmpleado);
            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EmpleadoCrudVM model)
        {
            bool emailExiste = await _appDbcontext.Empleados
                .AnyAsync(e => e.Email == model.Empleado.Email && e.Id != model.EmpleadoId);

            if (emailExiste)
            {
                ModelState.AddModelError("Empleado.Email", $"El email '{model.Empleado.Email}' ya existe.");
                model.Empleados = await _appDbcontext.Empleados.Include(e => e.Rol).ToListAsync();
                model.Roles = await _appDbcontext.Roles.ToListAsync();
                return View("Index", model);
            }

            var empleadoExistente = await _appDbcontext.Empleados.FindAsync(model.EmpleadoId);
            if (empleadoExistente == null)
                return RedirectToAction(nameof(Index));

            empleadoExistente.Nombre = model.Empleado.Nombre;
            empleadoExistente.Email = model.Empleado.Email;
            empleadoExistente.Telefono = model.Empleado.Telefono;
            empleadoExistente.Password = model.Empleado.Password;
            empleadoExistente.RolId = model.Empleado.RolId;

            await _appDbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Eliminar(int idEmpleado)
        {
            var empleado = await _appDbcontext.Empleados.FindAsync(idEmpleado);
            if (empleado != null)
            {
                _appDbcontext.Empleados.Remove(empleado);
                await _appDbcontext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
