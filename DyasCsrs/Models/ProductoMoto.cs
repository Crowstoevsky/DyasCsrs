    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DyasCsrs.Models
{
    public class ProductoMoto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Marca { get; set; }
        [Required, StringLength(50)]
        public string Modelo { get; set; }
        [Required, StringLength(6)]
        public string CC { get; set; }
        [Required, StringLength(4)]
        public string Anio { get; set; }
        [Required]
        public string Color { get; set; }
        [Required, Column(TypeName = "decimal(8,2)")]
        public decimal Precio { get; set; }
        public int ProveedorId { get; set; }
        [ForeignKey("ProveedorId")]
        public Proveedor Proveedor { get; set; }
        
        public int EstadoPMId { get; set; }
        [ForeignKey("EstadoPMId")]
        public virtual EstadoProductoMoto EstadoProductoMoto { get; set; }

        public ICollection<StockSucursal> StockSucursales { get; set; }
        public ICollection<DetallesVenta> DetallesVentas { get; set; }
    }
}