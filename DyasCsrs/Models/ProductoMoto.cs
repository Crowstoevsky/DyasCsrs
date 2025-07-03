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
        [RegularExpression(@"^[a-zA-Z0-9\s\-]+$", ErrorMessage = "Marca inválida (solo letras, números y guiones)")]
        public string Marca { get; set; }

        [Required, StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s\-]+$", ErrorMessage = "Modelo inválido (solo letras, números y guiones)")]
        public string Modelo { get; set; }

        [Required, StringLength(6)]
        [RegularExpression(@"^\d{2,6}$", ErrorMessage = "CC debe tener entre 2 y 6 dígitos numéricos")]
        public string CC { get; set; }

        [Required, StringLength(4)]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Año debe tener 4 dígitos")]
        public string Anio { get; set; }

        [Required, StringLength(30)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Color inválido (solo letras y espacios)")]
        public string Color { get; set; }

        [Required, Column(TypeName = "decimal(8,2)")]
        [Range(0.01, 999999.99, ErrorMessage = "Precio inválido")]
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