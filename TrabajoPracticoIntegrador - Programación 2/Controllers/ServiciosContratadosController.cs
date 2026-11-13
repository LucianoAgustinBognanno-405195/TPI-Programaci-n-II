using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrabajoPracticoIntegrador___Programación_2.DTOs;
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


        //PUT
        //10. ModificarExistente


        //11. CambioEstado
    }
}
