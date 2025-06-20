using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DyasCsrs.Models
{
    public class EstadoDevolucion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Nombre { get; set; }
        public ICollection<OpcionDevolucion> OpcionesDevolucion { get; set; }
    }
}
