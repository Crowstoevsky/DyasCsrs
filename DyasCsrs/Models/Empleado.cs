using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DyasCsrs.Models
{
    public class Empleado
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,StringLength(250)]
        public string Nombre { get; set; }
        [Required,StringLength(50)]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }
        [Required,StringLength(9)]
        public string Telefono { get; set; }
        [ForeignKey("RolId")]
        public int RolId { get; set; }
        public virtual Rol Rol { get; set; }
        public ICollection<Venta> Ventas { get; set; }
    }
}
