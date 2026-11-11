using TrabajoPracticoIntegrador___Programación_2.Models;

namespace TrabajoPracticoIntegrador___Programación_2.Services.Interfaces
{
    public interface IEmpleadosService
    {
        //GET
        //1. GetEmpleadosAll
        Task<List<Empleados>> GetEmpleadoAll();

        //2. GetEmpleadoByIdDNI
        Task<Empleados?> GetEmpleadoById(int idEmpl);

        //POST
        //3. NuevoEmpleado
        Task<bool> NuevEmpleado(Empleados nuevEmpl);

        //PUT
        //4. ModificarExistente
        Task<Empleados> UpdtEmpleado(int idEmpl, Empleados nuevEmpl);

        //5. BajaLogica
        Task<bool> BajaLogicaEmpleado(int idEmpl);
    }
}
