using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrabajoPracticoIntegrador___Programación_2.Models;
using TrabajoPracticoIntegrador___Programación_2.Services.Interfaces;
using TrabajoPracticoIntegrador___Programación_2.DTOs;
using TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Empleados;

namespace TrabajoPracticoIntegrador___Programación_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadosService _service;

        public EmpleadosController(IEmpleadosService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("by-EmpleadoALL")]
        public async Task<IActionResult> GetEmpleados()
        {
            try
            {
                List<Empleados> lst = await _service.GetEmpleadoAll();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(201, ex.Message);
            }
        }

        [HttpGet("By-EmpleadoById{idEmp}")]
        public async Task<IActionResult> GetEmpleadosId(int idEmp)
        {
            try
            {
                Empleados? empleados = await _service.GetEmpleadoById(idEmp);
                return Ok(empleados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("By-Sucursal")]
        public async Task<IActionResult> GetSucursal()
        {
            try
            {
                List<SucursalDto> lst = await _service.GetSucursal();
                return Ok(lst);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("By-Documentos")]
        public async Task<IActionResult> GetSucursales()
        {
            try
            {
                List<DocumentoDto> lst = await _service.GetDocumentos();
                return Ok(lst);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("By-Ciudades")]
        public async Task<IActionResult> GetCiudades()
        {
            try
            {
                List<CiudadDto> lst = await _service.GetCiudades();
                return Ok(lst);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("By-Barrios")]
        public async Task<IActionResult> GetBarrios(string Ciudad)
        {
            try
            {
                List<BarrioDto> lst = await _service.GetBarrios(Ciudad);
                return Ok(lst);
            }
            catch
            {
                return NotFound();
            }
        }


        [HttpPost("Nuevo-Empleado")]
        public async Task<IActionResult> NuevoEmpleado(EmpleadoDto dto)
        {
            try
            {
                bool r = await _service.NuevEmpleado(dto);
                return Ok("Se pudio");
            }
            catch (Exception e)
            {
                return StatusCode(201, e.Message);
            }
        }

        [HttpPut("Direccion-TipoEmpleado/{id}")]
        public async Task<IActionResult> PutEmpleadoDyT(int id, UpdateDyTEmpleadoDto dto)
        {
            try
            {
                bool r = await _service.UpdtEmpleadoDyT(id, dto);
                return Ok("Se modifico");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
