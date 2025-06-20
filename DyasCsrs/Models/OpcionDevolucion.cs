using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DyasCsrs.Models
{
    public class OpcionDevolucion
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Venta Venta { get; set; }
        public int VentaId { get; set; }
        public DateOnly FechaSolicitud { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required, StringLength(200)]
        public string Motivo { get; set; }

        [Required,StringLength(10)]
        public int EstadoDId { get; set; }
        [ForeignKey("EstadoDId")]
        public virtual EstadoDevolucion EstadoDevolucion { get; set; }

    }
}
