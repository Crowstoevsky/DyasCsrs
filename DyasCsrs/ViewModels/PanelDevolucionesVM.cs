using DyasCsrs.Models;

namespace DyasCsrs.ViewModels
{
    public class PanelDevolucionesVM
    {
        public List<OpcionDevolucion> DevolucionesPendientes { get; set; }
        public List<OpcionDevolucion> HistorialDevoluciones { get; set; }
    }


}