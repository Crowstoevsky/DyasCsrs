using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DyasCsrs.Models
{
    public class StockSucursal
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public ProductoMoto ProductoMoto { get; set; }
        public int ProductoMotoId { get; set; }
        public Sucursal Sucursal { get; set; }
        public int? SucursalId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }
        public bool Disponible => ProductoMoto?.EstadoPMId == 1 && Sucursal?.Activo == true; // solo si el producto está activo y la sucursal está activa

        public ICollection<DetallesVenta> DetallesVentas { get; set; }
    }
}
