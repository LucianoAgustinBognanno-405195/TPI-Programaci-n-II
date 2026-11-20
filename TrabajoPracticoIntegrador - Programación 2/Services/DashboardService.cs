using TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Dashboard;
using TrabajoPracticoIntegrador___Programación_2.Models;
using TrabajoPracticoIntegrador___Programación_2.Repositories.Interfaces;
using TrabajoPracticoIntegrador___Programación_2.Services.Interfaces;

namespace TrabajoPracticoIntegrador___Programación_2.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repo;

        public DashboardService(IDashboardRepository repo)
        {
            _repo = repo;
        }

        // Si el nombre ya está en la lista le suma 1, si no lo agrega con cantidad 1
        private void Contar(List<EstadisticaDto> lista, string nombre)
        {
            EstadisticaDto? item = lista.FirstOrDefault(e => e.Nombre == nombre);

            if (item == null)
            {
                lista.Add(new EstadisticaDto { Nombre = nombre, Cantidad = 1 });
            }
            else
            {
                item.Cantidad++;
            }
        }

        //1. Servicios más pedidos
        public async Task<List<EstadisticaDto>> GetServiciosMasPedidos(DateOnly? desde, DateOnly? hasta, int? idSucursal, int? idEmpleado)
        {
            List<Servicios> lst = await _repo.GetServiciosDashboard(desde, hasta, idSucursal, idEmpleado);
            List<EstadisticaDto> resultado = new List<EstadisticaDto>();

            foreach (Servicios s in lst)
            {
                Contar(resultado, s.IdTipoServicioNavigation.Descripcion);
            }

            return resultado.OrderByDescending(e => e.Cantidad).ToList();
        }

        //2. Empleados con más servicios
        public async Task<List<EstadisticaDto>> GetEmpleadosConMasServicios(DateOnly? desde, DateOnly? hasta, int? idSucursal, int? idEmpleado)
        {
            List<Servicios> lst = await _repo.GetServiciosDashboard(desde, hasta, idSucursal, idEmpleado);
            List<EstadisticaDto> resultado = new List<EstadisticaDto>();

            foreach (Servicios s in lst)
            {
                Contar(resultado, s.IdEmpleadoNavigation.Nombre + " " + s.IdEmpleadoNavigation.Apellido);
            }

            return resultado.OrderByDescending(e => e.Cantidad).ToList();
        }

        //3. Vehículos más usados
        public async Task<List<EstadisticaDto>> GetVehiculosMasUsados(DateOnly? desde, DateOnly? hasta, int? idSucursal, int? idEmpleado)
        {
            List<DetallesServicio> lst = await _repo.GetDetallesDashboard(desde, hasta, idSucursal, idEmpleado);
            List<EstadisticaDto> resultado = new List<EstadisticaDto>();

            foreach (DetallesServicio d in lst)
            {
                Contar(resultado, d.IdVehiculoNavigation.Descripcion);
            }

            return resultado.OrderByDescending(e => e.Cantidad).ToList();
        }

        //4. Servicios por mes
        public async Task<List<EstadisticaDto>> GetServiciosPorMes(DateOnly? desde, DateOnly? hasta, int? idSucursal, int? idEmpleado)
        {
            List<Servicios> lst = await _repo.GetServiciosDashboard(desde, hasta, idSucursal, idEmpleado);
            List<EstadisticaDto> resultado = new List<EstadisticaDto>();

            // Se recorren ordenados por fecha para que los meses queden en orden
            foreach (Servicios s in lst.OrderBy(s => s.FechaServicio))
            {
                string mes = s.FechaServicio.Month.ToString("00") + "/" + s.FechaServicio.Year;
                Contar(resultado, mes);
            }

            return resultado;
        }
    }
}