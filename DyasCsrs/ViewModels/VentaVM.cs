using DyasCsrs.Models;

namespace DyasCsrs.ViewModels
{
    public class VentaVM
    {
        public int EmpleadoId { get; set; } // empleado que realiza la venta
        public int ClienteId { get; set; } // cliente que realiza la compra
        public int ProductoId { get; set; } // producto seleccionado para la venta
        public int MetodoPagoId { get; set; } // metodo de pago seleccionado
        public Cliente Cliente { get; set; } // crud para cliente
        public ICollection<Cliente> Clientes { get; set; } // lista de clientes
        public ICollection<MetodoPago> MetodosPago { get; set; } // lista de metodos de pago
        public ICollection<ProductoMoto> ProductoMotos { get; set; } // lista de productos
        public List<DetallesVenta> Detalles { get; set; } = new List<DetallesVenta>(); // detalles de la venta
        public ICollection<Venta> Ventas { get; set; } // lista de ventas para historial
        public Venta Venta { get; set; } // venta actual
                                         // VentaVM.cs
        public int DetallesTempCantidad { get; set; }
        public int DetallesTempProductoId { get; set; }


    }
}
