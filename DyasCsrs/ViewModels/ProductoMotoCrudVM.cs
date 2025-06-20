using DyasCsrs.Models;
namespace DyasCsrs.ViewModels
{
    public class ProductoMotoCrudVM
    {
        public List<ProductoMoto> ProductosMotos { get; set; }
        public ProductoMoto ProductoMoto { get; set; }
        public int ProductoMotoId { get; set; }
        public List<Proveedor> Proveedores { get; set; }
        public int ProveedorId { get; set; }
        public List<EstadoProductoMoto> EstadosProductoMotos { get; set; }
        public int EstadosProductoId { get; set; }
    }
}
