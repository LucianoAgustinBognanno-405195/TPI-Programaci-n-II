using TrabajoPracticoIntegrador___Programación_2.Models;

namespace TrabajoPracticoIntegrador___Programación_2.DTOs
{
    public class ServicioCreateDTO
    {
        public int IdServicio { get; set; }

        public DateTime? FechaServicio { get; set; }

        public int? IdCliente { get; set; }

        public int? IdEmpleado { get; set; }

        public int? IdSucursal { get; set; }

        public string Descripcion { get; set; }

        public int? IdTipoServicio { get; set; }

        public int? EstadoServicio { get; set; }
    }
}
