using TrabajoPracticoIntegrador___Programación_2.Models;

namespace TrabajoPracticoIntegrador___Programación_2.Repositories.Interfaces
{
    public interface IEmpleadosRepository
    {
        //GET
        //1. GetEmpleadosAll
        Task<List<Empleados>> GetEmpleadoAll();

        //2. GetEmpleadoByIdDNI
        Task<Empleados?> GetEmpleadoById(int idEmpl);

        //3.GetEmpleadoDni
        Task<Empleados> GetEmpleadoByDni(int dni);

        //3. Get Sucursales
        Task<List<Sucursales>> GetSucursales();
        Task<List<TiposDocumentos>> GetTiposDocuments();

        Task<List<TiposEmpleados>> GetTiposEmpleados();

        //4. Get Documento
        Task<List<Documentos>> GetDocumentos();

        // 5. Get Ciudades
        Task<List<Ciudades>> GetCiudades();

        //6. Get Barrios

        Task<List<Barrios>> GetBarrios(string Ciudad);


        //POST
        //3. NuevoEmpleado
        Task<bool> NuevEmpleado(Empleados nuevEmpl);

        //PUT
        //4. ModificarExistente
        Task<bool> UpdtEmpleadoDyT(int idEmpl, Empleados nuevEmpl);

        //5. BajaLogica
        Task<bool> BajaLogicaEmpleado(int idEmpl);


        //Pedido de usario para login
        Task<Empleados?> GetEmpleadoByUsuario(string usuario);
    }
}