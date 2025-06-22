using DyasCsrs.Models;

namespace DyasCsrs.ViewModels
{
    public class DevolucionVM
    {
        public string DNICliente { get; set; }
        public Cliente ClienteEncontrado { get; set; }
        public List<Venta> VentasCliente { get; set; } = new List<Venta>();
        public OpcionDevolucion NuevaDevolucion { get; set; } = new();
        public string MensajeError { get; set; }

    }
}
