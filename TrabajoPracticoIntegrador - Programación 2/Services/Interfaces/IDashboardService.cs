using TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Dashboard;

namespace TrabajoPracticoIntegrador___Programación_2.Services.Interfaces
{
    public interface IDashboardService
    {
        //1. Servicios más pedidos
        Task<List<EstadisticaDto>> GetServiciosMasPedidos(DateOnly? desde, DateOnly? hasta, int? idSucursal, int? idEmpleado);

        //2. Empleados con más servicios
        Task<List<EstadisticaDto>> GetEmpleadosConMasServicios(DateOnly? desde, DateOnly? hasta, int? idSucursal, int? idEmpleado);

        //3. Vehículos más usados
        Task<List<EstadisticaDto>> GetVehiculosMasUsados(DateOnly? desde, DateOnly? hasta, int? idSucursal, int? idEmpleado);

        //4. Servicios por mes
        Task<List<EstadisticaDto>> GetServiciosPorMes(DateOnly? desde, DateOnly? hasta, int? idSucursal, int? idEmpleado);
    }
}