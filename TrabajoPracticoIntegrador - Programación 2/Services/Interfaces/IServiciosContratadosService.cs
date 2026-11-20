using TrabajoPracticoIntegrador___Programación_2.DTOs;
using TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Servicio;
using TrabajoPracticoIntegrador___Programación_2.Models;

namespace TrabajoPracticoIntegrador___Programación_2.Services.Interfaces
{
    public interface IServiciosContratadosService
    {
        //GET
        //1. GetServicioContratadoAll
        Task<List<ServicioListadoDTO>> GetSCAll();

        //2. GetServicioContratadoById
        Task<ServicioListadoDTO> GetSCById(int id);

        //3. GetServicioContratadoByFecha
        Task<List<ServicioListadoDTO>> GetSCByFecha(DateOnly fechaInicio, DateOnly? fechaFin);

        //4. GetServicioContratadoByCLiente
        Task<List<ServicioListadoDTO>> GetSCByCliente(int docCliente);

        //5. GetServicioContratadoByDNIEmpleado
        Task<List<ServicioListadoDTO>> GetSCByEmpleado(int dniEmpleado);

        //6. GetServicioContratadoBySucursal
        Task<List<ServicioListadoDTO>> GetSCBySucursal(string nombreSucursal);

        //7. GetServicioContratadoByEstado
        Task<List<ServicioListadoDTO>> GetSCByEstado(string estado);

        //8. GetDetalleServicioContratadoByIdServicioContratado
        Task<List<DetallesServicioListadoDTO>> GetDSCByIdSC(int idSC);

        //POST
        //9. NuevoServicio(con detalle)
        Task<int> NuevoSC(ServicioCreateDTO nuevSC);

        //PUT
        //10. ModificarExistente
        Task UpdateSC(int idSC, ServiceUpdatDto updtSC);

        //11. CambioEstado
        Task CambioEstadoSC(int idSC, int nuevEstado);

        //12. Modificar DetalleServicio
        Task UpdateDSC(int idSC, int idDetalle, DetalleUpdateDto dto);

        Task<List<ClienteComboDTO>> GetClientes();
        Task<List<TipoServicioComboDTO>> GetTiposServicio();
    
        Task<List<BateriaComboDTO>> GetBaterias();
        Task<List<VehiculoComboDTO>> GetVehiculos();
    }
}
