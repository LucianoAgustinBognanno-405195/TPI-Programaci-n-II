using TrabajoPracticoIntegrador___Programación_2.Models;

namespace TrabajoPracticoIntegrador___Programación_2.DTOs
{
    public class DetallesServicioListadoDTO
    {
        public int IdDetalleServicio { get; set; }

        public int IdBateria { get; set; }
        public string TipoBateria { get; set; }
        public string VoltajeBateria { get; set; }

        public int IdServicio { get; set; }
        public string DescVehiculo { get; set; }

        public string TipoVehiculo { get; set; }
    }
}
