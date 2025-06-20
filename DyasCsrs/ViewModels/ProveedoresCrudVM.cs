using DyasCsrs.Models;
namespace DyasCsrs.ViewModels
{
    public class ProveedoresCrudVM
    {
        public List<Proveedor> Proveedores { get; set; }
        public Proveedor Proveedor { get; set; } 
        public int ProveedorId { get; set; } 
    }
}
