using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DyasCsrs.Models
{
    public class Empleado
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(250)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Solo se permiten letras y espacios")]
        public string Nombre { get; set; }

        [Required, StringLength(50)]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido")]
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; }

        [Required, StringLength(9, MinimumLength = 9, ErrorMessage = "Debe tener exactamente 9 dígitos")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Solo se permiten números")]
        public string Telefono { get; set; }

        [ForeignKey("RolId")]
        public int? RolId { get; set; }
        public virtual Rol Rol { get; set; }

        public ICollection<Venta> Ventas { get; set; }
    }

}
