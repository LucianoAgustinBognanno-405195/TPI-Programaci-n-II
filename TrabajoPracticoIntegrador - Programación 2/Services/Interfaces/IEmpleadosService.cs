using TrabajoPracticoIntegrador___Programación_2.Models;

namespace TrabajoPracticoIntegrador___Programación_2.Services.Interfaces
{
    public interface IEmpleadosService
    {
        //GET
        //1. GetEmpleadosAll
        Task<List<Empleado>> GetEmpleadoAll();

        //2. GetEmpleadoByIdDNI
        Task<Empleado?> GetEmpleadoById(int idEmpl);

        //POST
        //3. NuevoEmpleado
        Task<bool> NuevEmpleado(Empleado nuevEmpl);

        //PUT
        //4. ModificarExistente
        Task<Empleado> UpdtEmpleado(int idEmpl, Empleado nuevEmpl);

        //5. BajaLogica
        Task<bool> BajaLogicaEmpleado(int idEmpl);
    }
}
