using DyasCsrs.Models;
using System.ComponentModel.DataAnnotations;

namespace DyasCsrs.ViewModels
{
    public class VentaVM
    {

        //// Para mostrar historial del vendedor
        //public List<Venta> HistorialVentas { get; set; } = new();

        //// Para llenar el combo de productos
        //public List<ProductoMoto> ProductosMotos { get; set; } = new();

        //// Para llenar el combo de sucursales
        //public List<Sucursal> Sucursales { get; set; } = new();

        //// Para llenar métodos de pago
        //public List<MetodoPago> MetodosPago { get; set; } = new();

        //// Para registrar venta: DNI del cliente
        //[Required]
        //[StringLength(8, MinimumLength = 8)]
        //public string DNICliente { get; set; }

        //// Lista de productos seleccionados en la venta
        //public List<ProductoVentaItem> Productos { get; set; } = new();
    }

    //public class ProductoVentaItem
    //{
    //    [Required]
    //    public int ProductoMotoId { get; set; }

    //    [Required]
    //    public int SucursalId { get; set; }

    //    [Required]
    //    [Range(1, 100)]
    //    public int Cantidad { get; set; }
    //}
}
