using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DyasCsrs.Models
{
    public class Rol
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        [Required,StringLength(50)]
        public string Nombre { get; set; }
        public ICollection<Empleado> Empleados { get; set; }
    }
}
