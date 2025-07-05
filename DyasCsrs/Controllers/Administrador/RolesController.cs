using DyasCsrs.Data;
using DyasCsrs.Models;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DyasCsrs.Controllers.Administrador
{
    [Authorize(Roles = "Administrador")]
    public class RolesController : Controller
    {
        private readonly AppDbContext _appDbcontext;

        public RolesController(AppDbContext context)
        {
            _appDbcontext = context;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new RolCrudVM
            {
                Roles = _appDbcontext.Roles.ToList(),
                Rol = new Rol() 
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(RolCrudVM model)
        {
            Rol nuevoRol = new Rol
            {
                Nombre = model.Rol.Nombre
            };

            await _appDbcontext.Roles.AddAsync(nuevoRol);
            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Editar(RolCrudVM model)
        {


            var rolExistente = await _appDbcontext.Roles.FindAsync(model.RolId);
            if (rolExistente == null)
                return RedirectToAction(nameof(Index));

            rolExistente.Nombre = model.Rol.Nombre;

            await _appDbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int idRol)
        {
            var rol = await _appDbcontext.Roles.FindAsync(idRol);
            if (rol != null)
            {
                var empleadosConRol = _appDbcontext.Empleados
                    .Where(e => e.RolId == idRol)
                    .ToList();

                foreach (var empleado in empleadosConRol)
                {
                    empleado.RolId = null;
                }

                _appDbcontext.Roles.Remove(rol);
                await _appDbcontext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
