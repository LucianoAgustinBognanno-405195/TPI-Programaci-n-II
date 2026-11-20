using Microsoft.EntityFrameworkCore;
using TrabajoPracticoIntegrador___Programación_2.Models;
using TrabajoPracticoIntegrador___Programación_2.Repositories.Interfaces;

namespace TrabajoPracticoIntegrador___Programación_2.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly CecchettoLogicaContext _dbContext;

        public DashboardRepository(CecchettoLogicaContext dbContext)
        {
            _dbContext = dbContext;
        }

        //1. Servicios del período
        public async Task<List<Servicios>> GetServiciosDashboard(DateOnly? desde, DateOnly? hasta, int? idSucursal, int? idEmpleado)
        {
            List<Servicios> lst = await _dbContext.Servicios.Include(s => s.IdTipoServicioNavigation)
                                                            .Include(s => s.IdEmpleadoNavigation)
                                                            .Where(s => (desde == null || s.FechaServicio >= desde) &&
                                                                        (hasta == null || s.FechaServicio <= hasta) &&
                                                                        (idSucursal == null || s.IdSucursal == idSucursal) &&
                                                                        (idEmpleado == null || s.IdEmpleado == idEmpleado))
                                                            .ToListAsync();
            return lst;
        }

        //2. Detalles de servicio del período (para los vehículos)
        public async Task<List<DetallesServicio>> GetDetallesDashboard(DateOnly? desde, DateOnly? hasta, int? idSucursal, int? idEmpleado)
        {
            List<DetallesServicio> lst = await _dbContext.DetallesServicio.Include(d => d.IdVehiculoNavigation)
                                                                          .Where(d => d.IdVehiculo != null &&
                                                                                      (desde == null || d.IdServicioNavigation.FechaServicio >= desde) &&
                                                                                      (hasta == null || d.IdServicioNavigation.FechaServicio <= hasta) &&
                                                                                      (idSucursal == null || d.IdServicioNavigation.IdSucursal == idSucursal) &&
                                                                                      (idEmpleado == null || d.IdServicioNavigation.IdEmpleado == idEmpleado))
                                                                          .ToListAsync();
            return lst;
        }
    }
}