using Microsoft.AspNetCore.Components.Endpoints.Infrastructure;
using TrabajoPracticoIntegrador___Programación_2.DTOs;
using TrabajoPracticoIntegrador___Programación_2.Models;
using TrabajoPracticoIntegrador___Programación_2.Repositories.Interfaces;
using TrabajoPracticoIntegrador___Programación_2.Services.Interfaces;

namespace TrabajoPracticoIntegrador___Programación_2.Services
{
    public class ServiciosContratadosService : IServiciosContratadosService
    {
        private readonly IServiciosContratadosRepository _repo;

        public ServiciosContratadosService(IServiciosContratadosRepository repo)
        {
            _repo = repo;
        }

        

        //GET
        //1. GetServicioContratadoAll
        public async Task<List<ServicioListadoDTO>> GetSCAll()
        {

            List<Servicios> lst = await _repo.GetSCAll();

            if (lst != null && lst.Count > 0)
            {
                return lst.Select(s => new ServicioListadoDTO
                {
                    IdServicio = s.IdServicio,
                    Fecha = s.FechaServicio,
                    Cliente = s.IdClienteNavigation.Nombre,
                    Empleado = s.IdEmpleadoNavigation.Nombre,
                    Sucursal = s.IdSucursalNavigation.Sucursal,
                    Descripcion = s.Descripcion,
                    TipoServicio = s.IdTipoServicioNavigation.Descripcion,
                    Estado = s.EstadoServicioNavigation.Estado
                }).ToList();
            }
            else
            {
                throw new Exception("No existen servicios contratados");
            }
        }


        //2. GetServicioContratadoById
        public async Task<Servicios?> GetSCById(int id)
        {
            Servicios? serv = await _repo.GetSCById(id);

            if (serv != null)
            {
                return (serv);
            }
            else
            {
                throw new Exception("No existe servicio contratado con ese Id");
            }
        }


        //3. GetServicioContratadoByFecha
        public async Task<List<Servicios>> GetSCByFecha(DateOnly fechaInicio, DateOnly? fechaFin)
        {
            List<Servicios> lst = await _repo.GetSCByFecha(fechaInicio, fechaFin);

            if (lst != null && lst.Count > 0)
            {
                return (lst);
            }
            else
            {
                throw new Exception("No existen servicios contratados a partir de esa fecha.");
            }
        }


        //4. GetServicioContratadoByCLiente
        public async Task<List<Servicios>> GetSCByCliente(int docCliente)
        {
            List<Servicios> lst = await _repo.GetSCByCliente(docCliente);

            if (lst != null && lst.Count > 0)
            {
                return (lst);
            }
            else
            {
                throw new Exception("No existen servicios contratados por ese cliente especificado.");
            }
        }


        //5. GetServicioContratadoByDNIEmpleado
        public async Task<List<Servicios>> GetSCByEmpleado(int docEmpleado)
        {
            List<Servicios> lst = await _repo.GetSCByEmpleado(docEmpleado);

            if (lst != null && lst.Count > 0)
            {
                return lst;
            }
            else
            {
                throw new Exception("No existen servicios contratados asociados a este empleado.");
            }
        }


        //6. GetServicioContratadoBySucursal
        public async Task<List<Servicios>> GetSCBySucursal(string nombreSucursal)
        {
            List<Servicios> lst = await _repo.GetSCBySucursal(nombreSucursal);

            if (lst != null && lst.Count > 0)
            {
                return lst;
            }
            else
            {
                throw new Exception("No existen servicios contratados asociados a esa sucursal.");
            }
        }


        //7. GetServicioContratadoByEstado
        public async Task<List<Servicios>> GetSCByEstado(string estado)
        {
            List<Servicios> lst = await _repo.GetSCByEstado(estado);

            if (lst != null && lst.Count > 0)
            {
                return lst;
            }
            else
            {
                throw new Exception($"No existen servicios contratados con estado {estado}.");
            }
        }


        //8. GetDetalleServicioContratadoByIdServicioContratado
        public async Task<List<DetallesServicioListadoDTO>> GetDSCByIdSC(int idSC)
        {
            List<DetallesServicio> lst = await _repo.GetDSCByIdSC(idSC);

            if (lst != null && lst.Count > 0)
            {
                return lst.Select(s => new DetallesServicioListadoDTO
                {
                    IdDetalleServicio = s.IdDetalleServicio,
                    IdBateria = s.IdBateria,
                    TipoBateria = s.IdBateriaNavigation.IdTipoBateriaNavigation.Tipos,
                    VoltajeBateria = s.IdBateriaNavigation.IdVoltajeNavigation.Voltaje,
                    IdServicio = s.IdServicioNavigation.IdServicio,
                    DescVehiculo = s.IdVehiculoNavigation.Descripcion,
                    TipoVehiculo = s.IdVehiculoNavigation.IdTipoVehiculoNavigation.Vehiculo,
                }).ToList();
            }
            else
            {
                throw new Exception("No existen detalles asociados a este Servicio.");
            }
        }



        //POST
        //9. NuevoServicio(con detalle)
        public async Task<bool> NuevoSC(Servicios nuevSC)
        {
            throw new NotImplementedException();
        }



        //PUT
        //10. ModificarExistente
        public async Task<Servicios> UpdateSC(int idSC, Servicios updtSC)
        {
            throw new NotImplementedException();
        }


        //11. CambioEstado
        public async Task<bool> CambioEstadoSC(int idSC, int nuevEstado)
        {
            throw new NotImplementedException();
        }

        //12. Modificar DetalleServicio
        public async Task<DetallesServicio> UpdateDSC(int idSC, DetallesServicio detServicio)
        {
            throw new NotImplementedException();
        }
    }
}
