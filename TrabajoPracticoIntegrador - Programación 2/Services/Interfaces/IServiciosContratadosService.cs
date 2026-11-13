using TrabajoPracticoIntegrador___Programación_2.DTOs;
using TrabajoPracticoIntegrador___Programación_2.Models;

namespace TrabajoPracticoIntegrador___Programación_2.Services.Interfaces
{
    public interface IServiciosContratadosService
    {
        //GET
        //1. GetServicioContratadoAll
        Task<List<ServicioListadoDTO>> GetSCAll();

        //2. GetServicioContratadoById
        Task<Servicios?> GetSCById(int id);

        //3. GetServicioContratadoByFecha
        Task<List<Servicios>> GetSCByFecha(DateOnly fechaInicio, DateOnly? fechaFin);

        //4. GetServicioContratadoByCLiente
        Task<List<Servicios>> GetSCByCliente(int docCliente);

        //5. GetServicioContratadoByDNIEmpleado
        Task<List<Servicios>> GetSCByEmpleado(int dniEmpleado);

        //6. GetServicioContratadoBySucursal
        Task<List<Servicios>> GetSCBySucursal(string nombreSucursal);

        //7. GetServicioContratadoByEstado
        Task<List<Servicios>> GetSCByEstado(string estado);

        //8. GetDetalleServicioContratadoByIdServicioContratado
        Task<List<DetallesServicioListadoDTO>> GetDSCByIdSC(int idSC);


        //POST
        //9. NuevoServicio(con detalle)
        Task<bool> NuevoSC(Servicios nuevSC);


        //PUT
        //10. ModificarExistente
        Task<Servicios> UpdateSC(int idSC, Servicios updtSC);

        //11. CambioEstado
        Task<bool> CambioEstadoSC(int idSC, int nuevEstado);

        //12. Modificar DetalleServicio
        Task<DetallesServicio> UpdateDSC(int idSC, DetallesServicio detServicio);
    }
}
