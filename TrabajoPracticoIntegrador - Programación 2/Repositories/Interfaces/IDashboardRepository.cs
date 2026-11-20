using TrabajoPracticoIntegrador___Programación_2.Models;

namespace TrabajoPracticoIntegrador___Programación_2.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        //1. Servicios del período
        Task<List<Servicios>> GetServiciosDashboard(DateOnly? desde, DateOnly? hasta, int? idSucursal, int? idEmpleado);

        //2. Detalles de servicio del período (para los vehículos)
        Task<List<DetallesServicio>> GetDetallesDashboard(DateOnly? desde, DateOnly? hasta, int? idSucursal, int? idEmpleado);
    }
}