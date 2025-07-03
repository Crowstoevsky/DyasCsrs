using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DyasCsrs.Models
{
    public class Sucursal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Solo se permiten letras y espacios")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La ubicación es obligatoria")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "La ubicación debe tener entre 5 y 100 caracteres")]
        [RegularExpression(@"^[\w\s\-,.#áéíóúÁÉÍÓÚñÑ]+$", ErrorMessage = "Ubicación inválida: usa letras, números, espacios y símbolos básicos")]
        public string Ubicacion { get; set; }

        public bool Activo { get; set; }

        public ICollection<StockSucursal> Stocks { get; set; }
    }
}
