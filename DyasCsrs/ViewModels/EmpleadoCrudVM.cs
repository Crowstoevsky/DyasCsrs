using DyasCsrs.Models;
using DyasCsrs.Models;
namespace DyasCsrs.ViewModels
{
    public class EmpleadoCrudVM
    {
        public List<Rol> Roles { get; set; }
        public List<Empleado> Empleados { get; set; }
        public Empleado Empleado { get; set; }
        public int EmpleadoId { get; set; }
    }
}
