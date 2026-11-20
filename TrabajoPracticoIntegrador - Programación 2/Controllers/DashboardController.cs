using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Dashboard;
using TrabajoPracticoIntegrador___Programación_2.Services.Interfaces;

namespace TrabajoPracticoIntegrador___Programación_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "administrativo,gerente,supervisor")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _serv;

        public DashboardController(IDashboardService serv)
        {
            _serv = serv;
        }

        //1. Servicios más pedidos
        [HttpGet("ServiciosMasPedidos")]
        public async Task<IActionResult> GetServiciosMasPedidos([FromQuery] DateOnly? desde, [FromQuery] DateOnly? hasta, [FromQuery] int? idSucursal, [FromQuery] int? idEmpleado)
        {
            try
            {
                List<EstadisticaDto> lst = await _serv.GetServiciosMasPedidos(desde, hasta, idSucursal, idEmpleado);
                return Ok(lst);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //2. Empleados con más servicios
        [HttpGet("EmpleadosConMasServicios")]
        public async Task<IActionResult> GetEmpleadosConMasServicios([FromQuery] DateOnly? desde, [FromQuery] DateOnly? hasta, [FromQuery] int? idSucursal, [FromQuery] int? idEmpleado)
        {
            try
            {
                List<EstadisticaDto> lst = await _serv.GetEmpleadosConMasServicios(desde, hasta, idSucursal, idEmpleado);
                return Ok(lst);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //3. Vehículos más usados
        [HttpGet("VehiculosMasUsados")]
        public async Task<IActionResult> GetVehiculosMasUsados([FromQuery] DateOnly? desde, [FromQuery] DateOnly? hasta, [FromQuery] int? idSucursal, [FromQuery] int? idEmpleado)
        {
            try
            {
                List<EstadisticaDto> lst = await _serv.GetVehiculosMasUsados(desde, hasta, idSucursal, idEmpleado);
                return Ok(lst);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //4. Servicios por mes
        [HttpGet("ServiciosPorMes")]
        public async Task<IActionResult> GetServiciosPorMes([FromQuery] DateOnly? desde, [FromQuery] DateOnly? hasta, [FromQuery] int? idSucursal, [FromQuery] int? idEmpleado)
        {
            try
            {
                List<EstadisticaDto> lst = await _serv.GetServiciosPorMes(desde, hasta, idSucursal, idEmpleado);
                return Ok(lst);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}