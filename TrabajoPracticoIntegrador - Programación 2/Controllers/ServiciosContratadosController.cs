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
            List<ServicioListadoDTO> lst = await _serv.GetSCAll();

            return Ok(lst);
        }


        //2. GetServicioContratadoById
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetSCById([FromRoute] int id)
        {
            try
            {
                ServicioListadoDTO serv = await _serv.GetSCById(id);

                return Ok(serv);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { mensaje = e.Message });
            }
            // Cualquier otro error (la base caída, por ejemplo) no lo atrapamos a propósito: .NET responde 500 por su cuenta, sin mostrarle detalles internos al usuario.
        }


        //3. GetServicioContratadoByFecha
        [HttpGet]
        [Route("ByFecha/{fechaInicio}")]
        public async Task<IActionResult> GetSCByFecha([FromRoute] DateOnly fechaInicio, [FromQuery] DateOnly? fechaFin)
        {
            try
            {
                List<ServicioListadoDTO> lst = await _serv.GetSCByFecha(fechaInicio, fechaFin);

                return Ok(lst);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { mensaje = e.Message });
            }

            //Los errores inesperados no los atrapamos a propósito: .NET responde 500 sin mostrarle detalles internos a nadie.
        }


        //4. GetServicioContratadoByCLiente
        [HttpGet]
        [Route("ByDocCliente/{docCliente}")]
        public async Task<IActionResult> GetSCByCliente([FromRoute] int docCliente)
        {
            try
            {
                List<ServicioListadoDTO> lst = await _serv.GetSCByCliente(docCliente);

                return Ok(lst);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }


        //5. GetServicioContratadoByDNIEmpleado
        [HttpGet]
        [Route("ByDocEmpleado/{docEmpleado}")]
        public async Task<IActionResult> GetSCByEmpleado([FromRoute] int docEmpleado)
        {
            try
            {
                List<ServicioListadoDTO> lst = await _serv.GetSCByEmpleado(docEmpleado);

                return Ok(lst);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }


        //6. GetServicioContratadoBySucursal
        [HttpGet]
        [Route("BySucursal/{nombreSucursal}")]
        public async Task<IActionResult> GetSCBySucursal([FromRoute] string nombreSucursal)
        {
            try
            {
                List<ServicioListadoDTO> lst = await _serv.GetSCBySucursal(nombreSucursal);

                return Ok(lst);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }


        //7. GetServicioContratadoByEstado
        [HttpGet]
        [Route("ByEstado/{estado}")]
        public async Task<IActionResult> GetSCByEstado([FromRoute] string estado)
        {
            try
            {
                List<ServicioListadoDTO> lst = await _serv.GetSCByEstado(estado);

                return Ok(lst);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }


        //8. GetDetalleServicioContratadoByIdServicioContratado
        [HttpGet]
        [Route("ByIdWithDetalles/{idSc}")]
        public async Task<IActionResult> GetDSCByIdSC([FromRoute] int idSc)
        {
            try
            {
                List<DetallesServicioListadoDTO> lst = await _serv.GetDSCByIdSC(idSc);

                return Ok(lst);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { mensaje = e.Message });
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
        public async Task<IActionResult> UpdateSC([FromRoute] int idSC, [FromBody] ServiceUpdatDto dto)
        {
            try
            {
                await _serv.UpdateSC(idSC, dto);

                return NoContent();   // 204: "Listo, todo bien"
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { mensaje = e.Message });     // 404: ese servicio no existe
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { mensaje = e.Message });   // 400: algún Id no existe
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { mensaje = e.Message });   // 400: el servicio ya está cerrado
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
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message }); // 400: "Tu pedido está mal"
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message }); // 400: ese estado no existe
            }
        }


        //12. Modificar DetalleServicio
        [HttpPut]
        [Route("{idSC:int}/detalles/{idDetalle:int}")]
        public async Task<IActionResult> UpdateDSC([FromRoute] int idSC, [FromRoute] int idDetalle, [FromBody] DetalleUpdateDto dto)
        {
            try
            {
                await _serv.UpdateDSC(idSC, idDetalle, dto);

                return NoContent();   // 204: "Listo, todo bien"
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { mensaje = e.Message });     // 404: no existe ese detalle
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { mensaje = e.Message });   // 400: batería o vehículo inexistente
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { mensaje = e.Message });   // 400: el servicio ya está cerrado
            }
        }


        //clientes para el desplegable del formulario de nuevo servicio
        [HttpGet("Clientes")]
        public async Task<IActionResult> GetClientes()
        {
            List<ClienteComboDTO> lst = await _serv.GetClientes();

            return Ok(lst);
        }

        //tipos de servicio para el desplegable del formulario de nuevo servicio
        [HttpGet("TiposServicio")]
        public async Task<IActionResult> GetTiposServicio()
        {
            List<TipoServicioComboDTO> lst = await _serv.GetTiposServicio();

            return Ok(lst);
        }
    }
}
