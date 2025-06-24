using DyasCsrs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace DyasCsrs.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public AuthController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var usuario = await _appDbContext.Empleados.Include(e => e.Rol).FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
            if (usuario == null)
            {
                TempData["Mensaje"] = "Credenciales incorrectas. Por favor, intente de nuevo.";
                return RedirectToAction(nameof(Login));
            }
            ;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email),

                new Claim(ClaimTypes.Role, usuario.Rol.Nombre),

            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);

            if (usuario.Rol.Nombre == "Administrador")            
                return RedirectToAction("Dashboard", "Admin");

            if (usuario.Rol.Nombre == "Gerente")
                return RedirectToAction("Dashboard", "Gerente");
            
            if (usuario.Rol.Nombre == "Vendedor")
                return RedirectToAction("Index", "Venta");

            return RedirectToAction("Index", "Home");
            
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Auth");
        }
    }
}
