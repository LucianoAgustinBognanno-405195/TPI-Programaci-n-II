using Microsoft.AspNetCore.Components.Endpoints.Infrastructure;
using Microsoft.EntityFrameworkCore;
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

        private const int ESTADO_PENDIENTE = 1; // el Id de "Pendiente" en tu tabla Estados_Servicio

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
        public async Task<int> NuevoSC(ServicioCreateDTO dtoSC)
        {
            // 1. Armo la ficha principal
            var servicio = new Servicios
            {
                FechaServicio = dtoSC.FechaServicio ?? DateOnly.FromDateTime(DateTime.Today),
                IdCliente = dtoSC.IdCliente,
                IdEmpleado = dtoSC.IdEmpleado,
                IdSucursal = dtoSC.IdSucursal,
                Descripcion = dtoSC.Descripcion,
                IdTipoServicio = dtoSC.IdTipoServicio,
                EstadoServicio = ESTADO_PENDIENTE
            };

            // 2. Por cada hoja de la notita, armo una hoja real y se la pego a la ficha
            foreach (var detalleDto in dtoSC.Detalles)
            {
                servicio.DetallesServicio.Add(new DetallesServicio
                {
                    IdBateria = detalleDto.IdBateria,
                    IdVehiculo = detalleDto.IdVehiculo
                    // IdServicio NO se completa: EF lo hace solo
                });
            }

            // 3. Guardo todo junto
            try
            {
                return await _repo.NuevoSC(servicio);
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException(
                    "El cliente, empleado, sucursal, tipo de servicio, batería o vehículo indicado no existe.");
            }
        }



        //PUT
        //10. ModificarExistente
        public async Task<Servicios> UpdateSC(int idSC, Servicios updtSC)
        {
            Servicios servicioModificado = await _repo.UpdateSC(idSC, updtSC);

            return servicioModificado;
        }


        //11. CambioEstado
        public async Task CambioEstadoSC(int idSC, int nuevEstado)
        {
            try
            {
                bool seCambio = await _repo.CambioEstadoSC(idSC, nuevEstado);

                if (!seCambio)
                    throw new KeyNotFoundException("Ese servicio no existe.");
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Ese estado no existe.");
            }
        }

        //12. Modificar DetalleServicio
        public async Task<DetallesServicio> UpdateDSC(int idSC, DetallesServicio detServicio)
        {
            return await _repo.UpdateDSC(idSC, detServicio);
        }
    
        //NUEVOCAMIO: clientes para el desplegable (id + "Nombre Apellido")
        public async Task<List<ClienteComboDTO>> GetClientes()
        {
            List<Clientes> lst = await _repo.GetClientes();

            return lst.Select(c => new ClienteComboDTO
            {
                IdCliente = c.IdCliente,
                Cliente = $"{c.Nombre} {c.Apellido}".Trim()
            }).ToList();
        }

        //NUEVOCAMIO: tipos de servicio para el desplegable
        public async Task<List<TipoServicioComboDTO>> GetTiposServicio()
        {
            List<TiposServicios> lst = await _repo.GetTiposServicio();

            return lst.Select(t => new TipoServicioComboDTO
            {
                IdTipoServicio = t.IdTipoServicio,
                Descripcion = t.Descripcion
            }).ToList();
        }
    }
}
