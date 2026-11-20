using TrabajoPracticoIntegrador___Programación_2.Models;
using TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Empleados;
using TrabajoPracticoIntegrador___Programación_2.DTOs;



namespace TrabajoPracticoIntegrador___Programación_2.Services.Interfaces
{
    public interface IEmpleadosService
    {
        //GET
        //1. GetEmpleadosAll
        Task<List<EmpleadoListadoDto>> GetEmpleadoAll();
        Task<List<EmpleadoListadoDto>> GetEmpleadoNav();

        //2. GetEmpleadoById
        Task<EmpleadoListadoDto?> GetEmpleadoById(int idEmpl);

        //3. GetEmpleadosDni
        Task<EmpleadoListadoDto?> GetEmpleadoByDni(int dni);

        //3. GetSucursales
        Task<List<SucursalDto>> GetSucursal();

        //4. GetDocumentos
        Task<List<DocumentoDto>> GetDocumentos();

        //5. Get Ciudades
        Task<List<CiudadDto>> GetCiudades();

        //6. Get Barrios
        Task<List<BarrioDto>> GetBarrios(string Ciudad);

        //7. Get Tipos de documentos
        Task<List<TiposDocumentos>> GetTiposDocuments();

        //8. Get Tipos de Empleados
        Task<List<TiposEmpleados>> GetTiposEmpleados();

        //POST
        //3. NuevoEmpleado
        Task<bool> NuevEmpleado(EmpleadoDto dto);

        //PUT
        //4. ModificarExistente
        Task<bool> UpdtEmpleadoDyT(int idEmpl, UpdateDyTEmpleadoDto dto);

        //5. BajaLogica
        Task<bool> BajaLogicaEmpleado(int idEmpl);

        //6. Alta Logica
        Task<bool> AltaLogicaEmpleado(int idEmpl);

        //login 
        Task<LoginRespuestaDto> Login(LoginDto dto);
    }
}
