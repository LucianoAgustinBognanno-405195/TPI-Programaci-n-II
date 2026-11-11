using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                List<Servicios> lst = await _serv.GetSCAll();

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
        public async Task<IActionResult> GetSCByFecha([FromRoute] DateTime fechaInicio, [FromQuery] DateTime? fechaFin)
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


        //6. GetServicioContratadoBySucursal


        //7. GetServicioContratadoByEstado


        //8. GetDetalleServicioContratadoByIdServicioContratado


        //POST
        //9. NuevoServicio(con detalle)


        //PUT
        //10. ModificarExistente


        //11. CambioEstado
    }
}
