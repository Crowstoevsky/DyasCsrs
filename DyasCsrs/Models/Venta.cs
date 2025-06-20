using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DyasCsrs.Models
{
    public class Venta
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado Empleado { get; set; }

        [ForeignKey("MetodoPagoId")]
        public MetodoPago MetodoPago { get; set; }

        [Required, Column(TypeName = "Decimal(8,2)")]
        public decimal Total { get; set; }

        public ICollection<DetallesVenta> Detalles { get; set; }
        public OpcionDevolucion OpcionDevolucion { get; set; }

    }
}
