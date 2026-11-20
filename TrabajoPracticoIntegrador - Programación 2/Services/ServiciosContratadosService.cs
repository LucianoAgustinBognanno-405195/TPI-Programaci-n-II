using Microsoft.AspNetCore.Components.Endpoints.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TrabajoPracticoIntegrador___Programación_2.DTOs;
using TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Servicio;
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
        private const int ESTADO_FINALIZADO = 3;
        private const int ESTADO_CANCELADO = 4;

        private static ServicioListadoDTO AListadoDto(Servicios s)
        {
            return new ServicioListadoDTO
            {
                IdServicio = s.IdServicio,
                Fecha = s.FechaServicio,
                Cliente = s.IdClienteNavigation != null
                                ? $"{s.IdClienteNavigation.Nombre} {s.IdClienteNavigation.Apellido}".Trim()
                                                : "Sin cliente",
                Empleado = s.IdEmpleadoNavigation != null
                                ? $"{s.IdEmpleadoNavigation.Nombre} {s.IdEmpleadoNavigation.Apellido}".Trim()
                                                : "Sin empleado",
                Sucursal = s.IdSucursalNavigation?.Sucursal ?? "Sin sucursal",
                Descripcion = s.Descripcion,
                TipoServicio = s.IdTipoServicioNavigation?.Descripcion ?? "Sin tipo",
                Estado = s.EstadoServicioNavigation?.Estado ?? "Sin estado"
            };
        }
        //Es la “máquina de pasar de ficha completa a ficha resumida”. La escribimos una vez y la reutilizamos.

        // Controla que el servicio exista y que todavía se pueda editar
        private async Task VerificarServicioEditableAsync(int idSC)
        {
            int? estado = await _repo.GetEstadoServicioAsync(idSC);

            if (estado == null)
                throw new KeyNotFoundException("No existe el servicio indicado con ese Id.");

            if (estado == ESTADO_FINALIZADO || estado == ESTADO_CANCELADO)
                throw new InvalidOperationException("No se puede modificar un servicio Cancelado o Finalizado.");
        }

        //GET
        //1. GetServicioContratadoAll
        public async Task<List<ServicioListadoDTO>> GetSCAll()
        {

            List<Servicios> lst = await _repo.GetSCAll();

            return lst.Select(AListadoDto).ToList();
        }

        //2. GetServicioContratadoById
        public async Task<ServicioListadoDTO> GetSCById(int id)
        {
            Servicios? s = await _repo.GetSCById(id);

            if (s == null)
                throw new KeyNotFoundException("No existe servicio contratado con ese Id");

            // Paso de "servicio completo" (entidad) a "servicio resumido" (DTO)
            return AListadoDto(s);

            //no lleva Select ni ToList. Esos son para listas.
        }

        //3. GetServicioContratadoByFecha
        public async Task<List<ServicioListadoDTO>> GetSCByFecha(DateOnly fechaInicio, DateOnly? fechaFin)
        {
            if (fechaFin.HasValue && fechaFin < fechaInicio)
                throw new ArgumentException("La fecha fin no puede ser anterior a la fecha de inicio.");
            // fechaFin.HasValue significa “¿la fecha de fin tiene un valor?”, ya que es opcional (DateOnly?).

            List<Servicios> lst = await _repo.GetSCByFecha(fechaInicio, fechaFin);

            return lst.Select(AListadoDto).ToList();
            //Select recorre la lista y le aplica AListadoDto a cada ficha, y ToList() arma la lista nueva con las fichas resumidas.
        }

        //4. GetServicioContratadoByCLiente
        public async Task<List<ServicioListadoDTO>> GetSCByCliente(int docCliente)
        {

            if (docCliente <= 0)
                throw new ArgumentException("El documento del cliente no es válido.");

            List<Servicios> lst = await _repo.GetSCByCliente(docCliente);

            return lst.Select(AListadoDto).ToList();
        }

        //5. GetServicioContratadoByDNIEmpleado
        public async Task<List<ServicioListadoDTO>> GetSCByEmpleado(int docEmpleado)
        {
            if (docEmpleado <= 0)
                throw new ArgumentException("El documento del empleado no es válido.");

            List<Servicios> lst = await _repo.GetSCByEmpleado(docEmpleado);

            return lst.Select(AListadoDto).ToList();
        }

        //6. GetServicioContratadoBySucursal
        public async Task<List<ServicioListadoDTO>> GetSCBySucursal(string nombreSucursal)
        {
            if (string.IsNullOrWhiteSpace(nombreSucursal))  
                throw new ArgumentException("El nombre de la sucursal es obligatorio.");    //string.IsNullOrWhiteSpace(...) pregunta:
                                                                                            //“¿este texto es null, está vacío o tiene solo espacios?”.
                                                                                            //Es mejor que comparar con "", porque también atrapa " " (tres espacios),
                                                                                            //que a simple vista parece vacío pero para C# es un texto con contenido.

            List<Servicios> lst = await _repo.GetSCBySucursal(nombreSucursal.Trim());
            // .Trim() le saca los espacios del principio y del final.
            // Si alguien escribe " Centro ", se busca "Centro". Sin eso, esa búsqueda
            // no encontraría nada y el usuario no entendería por qué.

            return lst.Select(AListadoDto).ToList();
        }

        //7. GetServicioContratadoByEstado
        public async Task<List<ServicioListadoDTO>> GetSCByEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new ArgumentException("El estado es obligatorio.");

            List<Servicios> lst = await _repo.GetSCByEstado(estado.Trim());

            return lst.Select(AListadoDto).ToList();
        }

        //8. GetDetalleServicioContratadoByIdServicioContratado
        public async Task<List<DetallesServicioListadoDTO>> GetDSCByIdSC(int idSC)
        {
            // 1. ¿Existe el servicio? Reutilizamos GetSCById, que ya tenemos en el Repository
            Servicios? servicio = await _repo.GetSCById(idSC);
            if (servicio == null)
                throw new KeyNotFoundException("No existe servicio contratado con ese Id");

            // 2. Traigo los detalles (si no tiene, la lista viene vacía y está bien)
            List<DetallesServicio> lst = await _repo.GetDSCByIdSC(idSC);

            return lst.Select(d => new DetallesServicioListadoDTO
            {
                IdDetalleServicio = d.IdDetalleServicio,
                IdBateria = d.IdBateria,
                TipoBateria = d.IdBateriaNavigation?.IdTipoBateriaNavigation?.Tipos ?? "Sin tipo de batería",
                VoltajeBateria = d.IdBateriaNavigation?.IdVoltajeNavigation?.Voltaje ?? "Sin voltaje",
                IdServicio = d.IdServicio,
                DescVehiculo = d.IdVehiculoNavigation?.Descripcion ?? "Sin vehículo",
                TipoVehiculo = d.IdVehiculoNavigation?.IdTipoVehiculoNavigation?.Vehiculo ?? "Sin tipo de vehículo"
            }).ToList();
        }

        //POST
        //9. NuevoServicio(con detalle)
        public async Task<int> NuevoSC(ServicioCreateDTO dtoSC)
        {
            if (!await _repo.EmpleadoActivoAsync(dtoSC.IdEmpleado))
                throw new ArgumentException("El empleado no existe o no está activo.");

            // 1. Armo el servicio principal
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
        public async Task UpdateSC(int idSC, ServiceUpdatDto dto)
        {
            await VerificarServicioEditableAsync(idSC);

            // Paso de la "notita" (DTO) a una ficha real (entidad)
            var serv = new Servicios
            {
                FechaServicio = dto.FechaServicio!.Value,
                IdCliente = dto.IdCliente,
                IdEmpleado = dto.IdEmpleado,
                IdSucursal = dto.IdSucursal,
                Descripcion = dto.Descripcion,
                IdTipoServicio = dto.IdTipoServicio
            };

            foreach (var detalleDto in dto.Detalles)
            {
                serv.DetallesServicio.Add(new DetallesServicio
                {
                    IdBateria = detalleDto.IdBateria,
                    IdVehiculo = detalleDto.IdVehiculo
                });
            }

            try
            {
                bool seCambio = await _repo.UpdateSC(idSC, serv);

                if (!seCambio)
                    throw new KeyNotFoundException("No existe el servicio indicado con ese Id.");
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("El cliente, empleado, sucursal o tipo de servicio indicado no existe.");
            }
        }

        //11. CambioEstado
        public async Task CambioEstadoSC(int idSC, int nuevEstado)
        {
            int seCambio;
            try
            {
                seCambio = await _repo.CambioEstadoSC(idSC, nuevEstado);
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Ese estado no existe.");
            }

            if (seCambio == 0)
            {
                throw new KeyNotFoundException("No existe servicio con ese id.");
            }
            else if (seCambio == 1)
            {
                throw new InvalidOperationException("No se puede cambiar el estado de servicios Cancelados o Finalizados.");
            }
        }

        //12. Modificar DetalleServicio
        public async Task UpdateDSC(int idSC, int idDetalle, DetalleUpdateDto dto)
        {
            await VerificarServicioEditableAsync(idSC);

            // Paso de la "notita" (DTO) a una hoja real (entidad)
            var det = new DetallesServicio
            {
                IdDetalleServicio = idDetalle,
                IdBateria = dto.IdBateria,
                IdVehiculo = dto.IdVehiculo
            };

            try
            {
                bool seCambio = await _repo.UpdateDSC(idSC, det);

                if (!seCambio)
                    throw new KeyNotFoundException("No existe ese detalle para el servicio indicado.");
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("La batería o el vehículo indicado no existe.");
            }
        }

        public async Task<List<ClienteComboDTO>> GetClientes()
        {
            List<Clientes> lst = await _repo.GetClientes();

            return lst.Select(c => new ClienteComboDTO
            {
                IdCliente = c.IdCliente,
                Cliente = $"{c.Nombre} {c.Apellido}".Trim(),
                Documento = c.IdDocumentoNavigation?.Documento
            }).ToList();
        }

        public async Task<List<TipoServicioComboDTO>> GetTiposServicio()
        {
            List<TiposServicios> lst = await _repo.GetTiposServicio();

            return lst.Select(t => new TipoServicioComboDTO
            {
                IdTipoServicio = t.IdTipoServicio,
                Descripcion = t.Descripcion
            }).ToList();
        }
    
        public async Task<List<BateriaComboDTO>> GetBaterias()
        {
            List<Baterias> lst = await _repo.GetBaterias();

            return lst.Select(b => new BateriaComboDTO
            {
                IdBateria = b.IdBateria,
                Descripcion = string.Join(" · ", new[]
                {
                    b.IdMarcaNavigation?.Descripcion,
                    b.IdTipoBateriaNavigation?.Tipos,
                    b.IdVoltajeNavigation?.Voltaje,
                    b.IdTamanioBateriaNavigation?.Grupo
                }.Where(x => !string.IsNullOrWhiteSpace(x)))
            }).ToList();
        }

        public async Task<List<VehiculoComboDTO>> GetVehiculos()
        {
            List<Vehiculos> lst = await _repo.GetVehiculos();

            return lst.Select(v => new VehiculoComboDTO
            {
                IdVehiculo = v.IdVehiculo,
                Descripcion = v.IdTipoVehiculoNavigation == null
                    ? v.Descripcion
                    : $"{v.Descripcion} ({v.IdTipoVehiculoNavigation.Vehiculo})"
            }).ToList();
        }
    }
}
