using System.ComponentModel.DataAnnotations;

namespace TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Servicio
{
    public class DetalleUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "La batería no es válida.")]
        public int IdBateria { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El vehículo no es válido.")]
        public int IdVehiculo { get; set; }

        //El Id del servicio y el del detalle van en la dirección, así que el cliente no puede mandar otros por error.
    }
}
