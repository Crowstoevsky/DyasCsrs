using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DyasCsrs.Models
{
    public class Cliente
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,StringLength(50)]
        public string Nombre { get; set; }
        [Required,StringLength(9)]
        public string Telefono { get; set; }
        [Required,StringLength(50)]
        public string Direccion {  get; set; }
        [Required,StringLength(8)]
        public string DNI {  get; set; }
        public ICollection<Venta> Ventas { get; set; }
    }
}
