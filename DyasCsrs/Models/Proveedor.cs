using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DyasCsrs.Models
{
    public class Proveedor
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        [Required, StringLength(50)]
        public string Nombre { get; set; }
        [Required,StringLength(9)]
        public string Telefono { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
        public ICollection<ProductoMoto> Productos { get; set; }
    }
}
