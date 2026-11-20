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

                Documento = new DocumentoDto
                {
                    Documento = e.IdDocumentoNavigation?.Documento ?? 0,
                    IdTipoDocumento = e.IdDocumentoNavigation.IdTipoDocumento ?? 0,
                },

                IdTipoEmpleado = e.IdTipoEmpleado,

                Ciudad = e.IdDireccionNavigation?
                            .IdBarrioNavigation?
                            .IdCiudadNavigation?.Ciudad,

                Direccion = e.IdDireccionNavigation == null ? null : new DireccionDto
                {
                    Calle = e.IdDireccionNavigation.Calle,
                    Altura = e.IdDireccionNavigation.Altura,
                    IdBarrio = e.IdDireccionNavigation.IdBarrio ?? 0
                }

            }).ToList();
        }
        
        public async Task<List<EmpleadoListadoDto>> GetEmpleadoNav()
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
                Documento = new DocumentoDto
                {
                    Documento = e.IdDocumentoNavigation?.Documento ?? 0
                    
                }

            }).ToList();
        }

        public async Task<EmpleadoListadoDto?> GetEmpleadoById(int idEmpl) // 2
        {
            if (idEmpl <= 0)
            {
                throw new Exception("El Id debe ser mayor a 0");
            }
            Empleados? empleados = await _repo.GetEmpleadoById(idEmpl);
            if (empleados == null)
            {
                throw new Exception("No existe ese empleado (getEmpleadoId service)");
            }
            
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
                    Documento = empleados.IdDocumentoNavigation?.Documento ?? 0,
                    IdTipoDocumento = empleados.IdDocumentoNavigation.IdTipoDocumento ?? 0,
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
            if (dni <= 0)
            {
                throw new Exception("El dni debe ser mayor a 0");
            }
            if (dni.ToString().Length < 7 || dni.ToString().Length > 8)
            {
                throw new Exception("El dni debe ser mayor a 1.000.000 y menor 100.000.000");
            }
            if (string.IsNullOrWhiteSpace(dni.ToString()))
            {
                throw new Exception("Debe ingresar un dni");
            }
            Empleados? empleados = await _repo.GetEmpleadoByDni(dni);
            if (empleados == null)
            {
                throw new Exception("No existe ese empleado (getEmpleadoDni service)");
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
            List<Sucursales?> lst = await _repo.GetSucursales();

            if(lst == null || lst.Count == 0)
            {
                throw new Exception("No se encontraron las sucursales");
            }

            return lst.Select(s => new SucursalDto
            {
                IdSucursal = s.IdSucursal,
                Sucursal = s.Sucursal,
            }).ToList();
        }

        public async Task<List<DocumentoDto>> GetDocumentos() //4
        {
            List<Documentos?> lst = await _repo.GetDocumentos();
            if (lst == null || lst.Count == 0)
            {
                throw new Exception("No se encontraron documentos cargados");
            }

            return lst.Select(d => new DocumentoDto
            {
                IdTipoDocumento = d.IdTipoDocumento,
                Documento = d.Documento,
            }).ToList();
        }
        public async Task<List<CiudadDto>> GetCiudades()
        {
            List<Ciudades?> lst = await _repo.GetCiudades();
            if (lst == null || lst.Count == 0)
            {
                throw new Exception("No se encontraron ciudades cargadas");
            }
            return lst.Select(c => new CiudadDto
            {
                Ciudad = c.Ciudad,
                IdCiudad = c.IdCiudad,
            }).ToList();

        }
        public async Task<List<TiposDocumentos>> GetTiposDocuments()
        {
            List<TiposDocumentos?> tDocumentos = await _repo.GetTiposDocuments();
            if (tDocumentos == null || tDocumentos.Count == 0)
            {
                throw new Exception("No hay tipos de documentos cargados");
            }
            return tDocumentos;
        }

        public async Task<List<TiposEmpleados>> GetTiposEmpleados()
        {
            List<TiposEmpleados?> tEmpleado = await _repo.GetTiposEmpleados();
            if (tEmpleado == null || tEmpleado.Count == 0)
            {
                throw new Exception("No hay tipos de empleados cargados");
            }
            return tEmpleado;
        }
        public async Task<List<BarrioDto>> GetBarrios(string Ciudad)
        {
            if (string.IsNullOrWhiteSpace(Ciudad))
            {
                throw new Exception("Debe ingresar una ciudad");
            }
            List<Barrios?> lst = await _repo.GetBarrios(Ciudad);

            if (lst == null || lst.Count == 0)
            {
                throw new Exception("No hay barrios cargados para esa ciudad");
            }
            return lst.Select(b => new BarrioDto
            {
                IdBarrio = b.IdBarrio,
                Barrio = b.Barrio,
                Ciudad = Ciudad
            }).ToList();
        }
        public async Task<bool> NuevEmpleado(EmpleadoDto dto) // 5
        {
            if (dto == null)
            {
                throw new Exception("Debe ingresar los datos del empleado");
            }

            if (dto.Documento == null)
            {
                throw new Exception("Debe ingresar los datos del documento");
            }

            if (dto.Direccion == null)
            {
                throw new Exception("Debe ingresar los datos de la dirección");
            }

            if (string.IsNullOrWhiteSpace(dto.Nombre))
            {
                throw new Exception("El nombre es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(dto.Apellido))
            {
                throw new Exception("El apellido es obligatorio");
            }

            if (!dto.Nombre.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                throw new Exception("El nombre solo puede contener letras");
            }

            if (!dto.Apellido.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                throw new Exception("El apellido solo puede contener letras");
            }

            if (dto.Documento.Documento <= 0)
            {
                throw new Exception("Debe ingresar un DNI válido");
            }

            if (dto.Documento.Documento.ToString().Length < 7 ||
                dto.Documento.Documento.ToString().Length > 8)
            {
                throw new Exception("El DNI debe tener entre 7 y 8 dígitos");
            }

            if (string.IsNullOrWhiteSpace(dto.Direccion.Calle))
            {
                throw new Exception("La calle es obligatoria");
            }

            if (dto.Direccion.Altura <= 0)
            {
                throw new Exception("La altura debe ser mayor a cero");
            }

            if (dto.Direccion.IdBarrio <= 0)
            {
                throw new Exception("Debe seleccionar un barrio");
            }

            if (string.IsNullOrWhiteSpace(dto.Usuario))
            {
                throw new Exception("El usuario es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(dto.Contrasenia))
            {
                throw new Exception("La contraseña es obligatoria");
            }

            if (dto.IdSucursal <= 0)
            {
                throw new Exception("Debe seleccionar una sucursal");
            }

            if (dto.IdTipoEmpleado <= 0)
            {
                throw new Exception("Debe seleccionar un tipo de empleado");
            }

            Empleados? empleadoExistente = await _repo.GetEmpleadoByDni(dto.Documento.Documento);
            if (empleadoExistente != null)
            {
                throw new Exception("Ya existe un empleado con ese DNI");
            }

            Empleados? usuarioExistente = await _repo.GetEmpleadoByUsuario(dto.Usuario);
            if (usuarioExistente != null)
            {
                throw new Exception("Ese nombre de usuario ya está registrado");
            }

            if (dto.Documento.IdTipoDocumento <= 0)
            {
                throw new Exception("Debe seleccionar un tipo de documento");
            }
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
            if (idEmpl <= 0)
            {
                throw new Exception("El ID del empleado debe ser mayor a cero");
            }
            if (dto == null)
            {
                throw new Exception("Debe ingresar los datos para actualizar");
            }

            if (dto.Direccion == null)
            {
                throw new Exception("Debe ingresar los datos de la dirección");
            }
            if (string.IsNullOrWhiteSpace(dto.Direccion.Calle))
            {
                throw new Exception("La calle es obligatoria");
            }

            if (dto.Direccion.Altura <= 0)
            {
                throw new Exception("La altura debe ser mayor a cero");
            }

            if (dto.Direccion.IdBarrio <= 0)
            {
                throw new Exception("Debe seleccionar un barrio");
            }
            if (dto.IdTipoEmpleado <= 0)
            {
                throw new Exception("Debe seleccionar un tipo de empleado");
            }
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
        public async Task<bool> BajaLogicaEmpleado(int idEmpl) // 7
        {
            if (idEmpl <= 0)
            {
                throw new Exception("El ID debe ser mayor a cero");
            }

            Empleados? empleado = await _repo.GetEmpleadoById(idEmpl);

            if (empleado == null)
            {
                throw new Exception("No existe ese empleado");
            }

            if (empleado.Activo == false)
            {
                throw new Exception("Ese empleado ya se encuentra dado de baja");
            }

            return await _repo.BajaLogicaEmpleado(idEmpl);
        }
        public async Task<bool> AltaLogicaEmpleado(int idEmpl)
        {
            if (idEmpl <= 0)
            {
                throw new Exception("El ID debe ser mayor a cero");
            }

            Empleados? empleado = await _repo.GetEmpleadoById(idEmpl);

            if (empleado == null)
            {
                throw new Exception("No existe ese empleado");
            }

            if (empleado.Activo == true)
            {
                throw new Exception("Ese empleado no se dio de baja");
            }

            return await _repo.AltaLogicaEmpleado(idEmpl);
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
        new Claim(ClaimTypes.Name, empleado.Usuario),

            new Claim(ClaimTypes.Role, empleado.IdTipoEmpleadoNavigation.TipoEmpleado
    )
        
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
