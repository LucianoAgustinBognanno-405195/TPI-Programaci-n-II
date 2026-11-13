using TrabajoPracticoIntegrador___Programación_2.Models;
using TrabajoPracticoIntegrador___Programación_2.Repositories.Interfaces;
using TrabajoPracticoIntegrador___Programación_2.Services.Interfaces;
using TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Empleados;
using TrabajoPracticoIntegrador___Programación_2.DTOs;

namespace TrabajoPracticoIntegrador___Programación_2.Services
{
    public class EmpleadoService : IEmpleadosService
    {
        private readonly IEmpleadosRepository _repo;
        public EmpleadoService(IEmpleadosRepository repo)
        {
            this._repo = repo;
        }


        public async Task<List<Empleados>> GetEmpleadoAll() // 1
        {
            List<Empleados> lst = await _repo.GetEmpleadoAll();

            if (lst == null || lst.Count == 0)
            {
                throw new Exception("No hay empleados cargados");
            }
            return lst;
        }

        public async Task<Empleados?> GetEmpleadoById(int idEmpl) // 2
        {
            Empleados? empleados = await _repo.GetEmpleadoById(idEmpl);
            return empleados;
        }
        public async Task<List<SucursalDto>> GetSucursal() //3
        {
            List<Sucursales> lst = await _repo.GetSucursales();

            return lst.Select(s => new SucursalDto
            {
                IdSucursal = s.IdSucursal,
                Sucursal = s.Sucursal,
            }).ToList();
        }

        public async Task<List<DocumentoDto>> GetDocumentos() //4
        {
            List<Documentos> lst = await _repo.GetDocumentos();

            return lst.Select(d => new DocumentoDto
            {
                IdTipoDocumento = d.IdTipoDocumento,
                Documento = d.Documento,
            }).ToList();
        }
        public async Task<List<CiudadDto>> GetCiudades()
        {
            List<Ciudades> lst = await _repo.GetCiudades();
            return lst.Select(c => new CiudadDto
            {
                Ciudad = c.Ciudad,
                IdCiudad = c.IdCiudad,
            }).ToList();

        }
        public async Task<List<BarrioDto>> GetBarrios(string Ciudad)
        {
            List<Barrios> lst = await _repo.GetBarrios(Ciudad);
            return lst.Select(b => new BarrioDto
            {
                IdBarrio = b.IdBarrio,
                Barrio = b.Barrio,
                Ciudad = Ciudad
            }).ToList();
        }
        public async Task<bool> NuevEmpleado(EmpleadoDto dto) // 5
        {
            Documentos documento = new Documentos()
            {
                Documento = dto.Documento.Documento,
                IdTipoDocumento = dto.Documento.IdTipoDocumento
            };
            Direcciones direcciones = new Direcciones()
            {
                Calle = dto.Direccion.Calle,
                Altura = dto.Direccion.Altura,
                IdBarrio = dto.Direccion.IdBarrio
            };
            Empleados nuevoEmpleado = new Empleados()
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                IdSucursal = dto.IdSucursal,
                IdTipoEmpleado = dto.IdTipoEmpleado,
                Usuario = dto.Usuario,
                Contrasenia = dto.Contrasenia,
                Activo = true,

                IdDireccionNavigation = direcciones,
                IdDocumentoNavigation = documento
            };
            return await _repo.NuevEmpleado(nuevoEmpleado);
        }

        public async Task<bool> UpdtEmpleadoDyT(int idEmpl, UpdateDyTEmpleadoDto dto) // 6
        {
            Empleados empleados = new Empleados()
            {
                IdTipoEmpleado = dto.IdTipoEmpleado,
                IdDireccionNavigation = new Direcciones()
                {
                    Calle = dto.Direccion.Calle,
                    Altura = dto.Direccion.Altura,
                    IdBarrio = dto.Direccion.IdBarrio
                }
            };
            return await _repo.UpdtEmpleadoDyT(idEmpl, empleados);

        }
        public Task<bool> BajaLogicaEmpleado(int idEmpl) // 7
        {
            throw new NotImplementedException();
        }

    }
}
