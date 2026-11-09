using TrabajoPracticoIntegrador___Programación_2.Models;

namespace TrabajoPracticoIntegrador___Programación_2.Repositories.Interfaces
{
    public interface IServiciosContratadosRepository
    {
        //GET
        //1. GetServicioContratadoAll
        Task<List<Servicio>> GetSCAll();

        //2. GetServicioContratadoById
        Servicio? GetSCById(int id);

        //3. GetServicioContratadoByFecha
        Task<List<Servicio>> GetSCByFecha(DateTime fecha);

        //4. GetServicioContratadoByCLiente (agregar dni/cuil) (agregar tipo_dni)
        Task<List<Servicio>> GetSCByCliente(int dniCliente);

        //5. GetServicioContratadoByDNIEmpleado
        Task<List<Servicio>> GetSCByEmpleado(int dniEmpleado);

        //6. GetServicioContratadoBySucursal
        Task<List<Servicio>> GetSCBySucursal(string nombreSucursal);

        //7. GetServicioContratadoByEstado
        Task<List<Servicio>> GetSCByEstado(string estado);

        //8. GetDetalleServicioContratadoByIdServicioContratado
        Task<List<DetallesServicio>> GetDSCByIdSC(int idSC);


        //POST
        //9. NuevoServicio(con detalle)
        Task<bool> NuevoSC(Servicio nuevSC);


        //PUT
        //10. ModificarExistente
        Task<Servicio> UpdateSC(int idSC, Servicio updtSC);

        //11. CambioEstado
        Task<bool> CambioEstadoSC(int idSC, string nuevEstado);
    }

}












   
