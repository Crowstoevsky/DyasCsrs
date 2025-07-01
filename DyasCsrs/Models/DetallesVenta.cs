using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DyasCsrs.Models
{
    public class DetallesVenta
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StockSucursalId { get; set; }

        [ForeignKey("StockSucursalId")]
        public StockSucursal StockSucursal { get; set; }

        public int CompraId { get; set; }

        [ForeignKey("CompraId")]
        public Venta Venta { get; set; }
        [ForeignKey("ProductoMotoID")]
        public int ProductoMotoID { get; set; }
        public ProductoMoto ProductoMoto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required,Column(TypeName ="Decimal(8,2)")]
        public decimal PrecioUnitario { get; set; }
        [Required, Column(TypeName = "Decimal(8,2)")]
        public decimal SubTotal { get; set; }
    }
}
