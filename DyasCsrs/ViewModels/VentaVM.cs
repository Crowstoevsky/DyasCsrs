using DyasCsrs.Models;

namespace DyasCsrs.ViewModels
{
    public class VentaVM
    {
        public int EmpleadoId { get; set; }
        public int ClienteId { get; set; }
        public int ProductoId { get; set; } 
        public int MetodoPagoId { get; set; }
        public int StockSucursalId { get; set; }
        public Cliente Cliente { get; set; } = new();
        public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
        public ICollection<MetodoPago> MetodosPago { get; set; } = new List<MetodoPago>();
        public ICollection<StockSucursal> StockDisponible { get; set; } = new List<StockSucursal>();
        public List<DetallesVenta> Detalles { get; set; } = new(); 

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
        public Venta Venta { get; set; } = new();

        public int DetallesTempCantidad { get; set; } = 1; 
    }

}
