using DyasCsrs.Models;
namespace DyasCsrs.ViewModels
{
    public class StockCrudVM
    {
        public List<StockSucursal> Stocks { get; set; }
        public StockSucursal Stock { get; set; }
        public int StockId { get; set; }
        public List<ProductoMoto> ProductosMotos { get; set; }
        public int ProductoMotoId { get; set; }
        public List<Sucursal> Sucursales { get; set; }
        public int SucursalId { get; set; }
    }
}
