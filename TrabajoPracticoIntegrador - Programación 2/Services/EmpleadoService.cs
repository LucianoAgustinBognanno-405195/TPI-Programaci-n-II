using TrabajoPracticoIntegrador___Programación_2.Models;
using TrabajoPracticoIntegrador___Programación_2.Repositories.Interfaces;
using TrabajoPracticoIntegrador___Programación_2.Services.Interfaces;
using TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Empleados;
using TrabajoPracticoIntegrador___Programación_2.DTOs;

//jwt 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace TrabajoPracticoIntegrador___Programación_2.Services
{
    public class EmpleadoService : IEmpleadosService
    {
        private readonly IEmpleadosRepository _repo;
        private readonly IConfiguration _configuration;

        public EmpleadoService(
            IEmpleadosRepository repo,
            IConfiguration configuration)
        {
            this._repo = repo;
            this._configuration = configuration;
        }


        public async Task<List<EmpleadoListadoDto>> GetEmpleadoAll()
        {
            List<Empleados> lst = await _repo.GetEmpleadoAll();

            if (lst == null || lst.Count == 0)
            {
                throw new Exception("No hay empleados cargados");
            }
           

            return lst.Select(e => new EmpleadoListadoDto
            {

                IdEmpleado = e.IdEmpleado,
                Nombre = e.Nombre,
                Apellido = e.Apellido,
                Usuario = e.Usuario,
                Activo = e.Activo,
                //NUEVOCAMIO: si el empleado no tiene documento, queda en 0 en vez de romper el listado
                Documento = new DocumentoDto
                {
                    Documento = e.IdDocumentoNavigation?.Documento ?? 0
                    
                }

            }).ToList();
        }

        public async Task<EmpleadoListadoDto?> GetEmpleadoById(int idEmpl) // 2
        {
            Empleados? empleados = await _repo.GetEmpleadoById(idEmpl);
            if (empleados == null)
            {
                throw new Exception("No existe ese empleado");
            }

            //NUEVOCAMIO: agregamos tipo de empleado, ciudad y direccion para precargar el formulario de edicion
            Direcciones? direccion = empleados.IdDireccionNavigation;

            return new EmpleadoListadoDto
            {
                IdEmpleado = empleados.IdEmpleado,
                Nombre = empleados.Nombre,
                Apellido = empleados.Apellido,
                Usuario = empleados.Usuario,
                Activo = empleados.Activo,
                Documento = new DocumentoDto
                {
                    Documento = empleados.IdDocumentoNavigation?.Documento ?? 0
                },
                IdTipoEmpleado = empleados.IdTipoEmpleado,
                Ciudad = direccion?.IdBarrioNavigation?.IdCiudadNavigation?.Ciudad,
                Direccion = direccion == null ? null : new DireccionDto
                {
                    Calle = direccion.Calle,
                    Altura = direccion.Altura,
                    IdBarrio = direccion.IdBarrio ?? 0
                }
            };
        }
        public async Task<EmpleadoListadoDto?> GetEmpleadoByDni(int dni) // 2
        {
            Empleados? empleados = await _repo.GetEmpleadoById(dni);
            if (empleados == null)
            {
                throw new Exception("No existe ese empleado");
            }

            return new EmpleadoListadoDto
            {
                IdEmpleado = empleados.IdEmpleado,
                Nombre = empleados.Nombre,
                Apellido = empleados.Apellido,
                Usuario = empleados.Usuario,
                Activo = empleados.Activo,
                Documento = new DocumentoDto
                {
                    Documento = empleados.IdDocumentoNavigation.Documento
                }

            };
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
        public async Task<List<TiposDocumentos>> GetTiposDocuments()
        {
            List<TiposDocumentos> tDocumentos = await _repo.GetTiposDocuments();
            return tDocumentos;
        }

        public async Task<List<TiposEmpleados>> GetTiposEmpleados()
        {
            List<TiposEmpleados> tEmpleado = await _repo.GetTiposEmpleados();
            return tEmpleado;
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
            
            return _repo.BajaLogicaEmpleado(idEmpl);
        }

        
       public async Task<LoginRespuestaDto> Login(LoginDto dto)
        {
            // 1. Buscamos al empleado por su usuario
            Empleados? empleado = await _repo.GetEmpleadoByUsuario(dto.Usuario);

            // 2. Verificamos que exista y que esté activo
            if (empleado == null || !empleado.Activo)
            {
                throw new Exception("Usuario o contraseña incorrectos");
            }

            // 3. Verificamos la contraseña (comparación temporal)
            if (empleado.Contrasenia != dto.Contrasenia)
            {
                throw new Exception("Usuario o contraseña incorrectos");
            }

            // 4. Creamos los datos que tendrá el token
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, empleado.IdEmpleado.ToString()),
        new Claim(ClaimTypes.Name, empleado.Usuario)
            };

            // 5. Obtenemos la clave secreta
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            // 6. Creamos la firma
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            // 7. Generamos el token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            // 8. Devolvemos la respuesta
            return new LoginRespuestaDto
            {
                IdEmpleado = empleado.IdEmpleado,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido,
                Usuario = empleado.Usuario,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

      
    }
}
