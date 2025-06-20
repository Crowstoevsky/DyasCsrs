using DyasCsrs.Data;
using DyasCsrs.ViewModels;
using DyasCsrs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DyasCsrs.Controllers.Administrador
{
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
                Empleado = new Empleado() // vacío para formulario
            };
            return View(empleadoCrud);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(EmpleadoCrudVM model)
        {
            Empleado nuevoEmpleado = new Empleado
            {
                Nombre = model.Empleado.Nombre,
                Email = model.Empleado.Email,
                Password = model.Empleado.Password,
                Telefono = model.Empleado.Telefono,
                RolId = model.Empleado.RolId // Asignación directa por Id
            };

            await _appDbcontext.Empleados.AddAsync(nuevoEmpleado);
            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EmpleadoCrudVM model)
        {


            var empleadoExistente = await _appDbcontext.Empleados.FindAsync(model.EmpleadoId);
            if (empleadoExistente == null)
                return RedirectToAction(nameof(Index));

            empleadoExistente.Nombre = model.Empleado.Nombre;
            empleadoExistente.Email = model.Empleado.Email;
            empleadoExistente.Password = model.Empleado.Password;
            empleadoExistente.Telefono = model.Empleado.Telefono;
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
