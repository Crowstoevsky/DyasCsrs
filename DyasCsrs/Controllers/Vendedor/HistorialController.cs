using DyasCsrs.Data;
using DyasCsrs.Services;
using DyasCsrs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DyasCsrs.Controllers.Vendedor
{
    [Authorize(Roles = "Vendedor")]
    public class HistorialController : Controller
    {
        private readonly AppDbContext _appDbcontext;
        
        public HistorialController(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var vm = new HistorialVM
            {
                Ventas = _appDbcontext.Ventas.Include(v => v.Cliente)
                                              .Include(v => v.Detalles)
                                              .ThenInclude(d => d.ProductoMoto)
                                              .ToList()
            };
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> DescargarComprobante(int ventaId)
        {
            var venta = await _appDbcontext.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.MetodoPago)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.ProductoMoto)
                .FirstOrDefaultAsync(v => v.Id == ventaId);

            if (venta == null) return NotFound();

            var pdfService = new PdfGeneratorService();
            var pdfBytes = pdfService.GenerarComprobanteVenta(venta);

            return File(pdfBytes, "application/pdf", $"Comprobante_Venta_{ventaId}.pdf");
        }
    }
}
