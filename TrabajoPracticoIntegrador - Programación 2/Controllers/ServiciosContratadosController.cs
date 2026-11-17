using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrabajoPracticoIntegrador___Programación_2.DTOs;
using TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Servicio;
using TrabajoPracticoIntegrador___Programación_2.Models;
using TrabajoPracticoIntegrador___Programación_2.Services.Interfaces;

namespace TrabajoPracticoIntegrador___Programación_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosContratadosController : ControllerBase
    {
        private readonly IServiciosContratadosService _serv;
        public ServiciosContratadosController(IServiciosContratadosService _serv)
        {
            this._serv = _serv;
        }



        //GET
        //1. GetServicioContratadoAll
        [HttpGet]
        public async Task<IActionResult> GetSCAll()
        {
            try
            {
                List<ServicioListadoDTO> lst = await _serv.GetSCAll();

                return Ok(lst);
            }
            catch (Exception e)
            {
                return StatusCode(201, e.Message);
            }
        }


        //2. GetServicioContratadoById
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSCById([FromRoute] int id)
        {
            try
            {
                Servicios? serv = await _serv.GetSCById(id);

                return Ok(serv);
            }
            catch (Exception e)
            {
                return StatusCode(201, e.Message);
            }
        }


        //3. GetServicioContratadoByFecha
        [HttpGet]
        [Route("ByFecha/{fechaInicio}")]
        public async Task<IActionResult> GetSCByFecha([FromRoute] DateOnly fechaInicio, [FromQuery] DateOnly? fechaFin)
        {
            try
            {
                List<Servicios> lst = await _serv.GetSCByFecha(fechaInicio, fechaFin);

                return Ok(lst);
            }
            catch (Exception e)
            {
                return StatusCode(201, e.Message);
            }
        }


        //4. GetServicioContratadoByCLiente
        [HttpGet]
        [Route("ByDocCliente/{docCliente}")]
        public async Task<IActionResult> GetSCByCliente([FromRoute] int docCliente)
        {
            try
            {
                List<Servicios> lst = await _serv.GetSCByCliente(docCliente);

                return Ok(lst);
            }
            catch (Exception e)
            {
                return StatusCode(201, e.Message);
            }
        }


        //5. GetServicioContratadoByDNIEmpleado
        [HttpGet]
        [Route("ByDocEmpleado/{docEmpleado}")]
        public async Task<IActionResult> GetSCByEmpleado([FromRoute] int docEmpleado)
        {
            try
            {
                List<Servicios> lst = await _serv.GetSCByEmpleado(docEmpleado);

                return Ok(lst);
            }
            catch (Exception e)
            {

                return StatusCode(201, e.Message);
            }
        }


        //6. GetServicioContratadoBySucursal
        [HttpGet]
        [Route("BySucursal/{nombreSucursal}")]
        public async Task<IActionResult> GetSCBySucursal([FromRoute] string nombreSucursal)
        {
            try
            {
                List<Servicios> lst = await _serv.GetSCBySucursal(nombreSucursal);

                return Ok(lst);
            }
            catch (Exception e)
            {

                return StatusCode(201, e.Message);
            }
        }


        //7. GetServicioContratadoByEstado
        [HttpGet]
        [Route("ByEstado/{estado}")]
        public async Task<IActionResult> GetSCByEstado([FromRoute] string estado)
        {
            try
            {
                List<Servicios> lst = await _serv.GetSCByEstado(estado);

                return Ok(lst);
            }
            catch (Exception e)
            {

                return StatusCode(201, e.Message);
            }
        }


        //8. GetDetalleServicioContratadoByIdServicioContratado
        [HttpGet]
        [Route("ByIdServicio/{idSc}")]
        public async Task<IActionResult> GetDSCByIdSC([FromRoute] int idSc)
        {
            try
            {
                List<DetallesServicioListadoDTO> lst = await _serv.GetDSCByIdSC(idSc);

                return Ok(lst);
            }
            catch (Exception e)
            {

                return StatusCode(201, e.Message);
            }
        }



        //POST
        //9. NuevoServicio(con detalle)
        [HttpPost]
        public async Task<IActionResult> InsertarServicio([FromBody] ServicioCreateDTO dto)
        {
            try
            {
                int nuevoId = await _serv.NuevoSC(dto); //nuevo id porq voy a mostrar el id del nuevo servicio
                return StatusCode(201, new { idServicio = nuevoId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        //PUT
        //10. ModificarExistente
        [HttpPut]
        [Route("{idSC}")]
        public async Task<IActionResult> UpdateSC([FromRoute] int idSC, [FromBody] Servicios updtSC)
        {
            try
            {
                Servicios servicioModificado = await _serv.UpdateSC(idSC, updtSC);

                return Ok(servicioModificado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //11. CambioEstado
        [HttpPatch("{idSc}/estado")]
        public async Task<IActionResult> CambiarEstado(int idSc, CambioEstadoDto dto)
        {
            try
            {
                await _serv.CambioEstadoSC(idSc, dto.NuevoEstado);
                return NoContent();                          // 204: "Listo, todo bien"
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });   // 404: "Ese servicio no existe"
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message }); // 400: "Tu pedido está mal"
            }
        }


        //12. Modificar DetalleServicio
        [HttpPut]
        [Route("UpdateDetalle/{idSC}")]
        public async Task<IActionResult> UpdateDSC([FromRoute] int idSC, [FromBody] DetallesServicio detServicio)
        {
            try
            {
                DetallesServicio det = await _serv.UpdateDSC(idSC, detServicio);
                return Ok(det);
            }
            catch (Exception e)
            {
                return StatusCode(201, e.Message);
            }
        }

    }
}
