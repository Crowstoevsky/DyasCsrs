using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DyasCsrs.Models
{
    public class Proveedor
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^\d{7,15}$", ErrorMessage = "Solo números (7-15 dígitos)")]
        public string Telefono { get; set; }
        public ICollection<ProductoMoto> Productos { get; set; }
    }
}
