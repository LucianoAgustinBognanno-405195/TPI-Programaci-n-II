namespace TrabajoPracticoIntegrador___Programación_2.DTOs
{
    public class ServiceUpdatDto
    {
        public DateOnly FechaServicio { get; set; }

        public int IdCliente { get; set; }

        public int IdEmpleado { get; set; }

        public int IdSucursal { get; set; }

        public string Descripcion { get; set; }

        public int IdTipoServicio { get; set; }
    }
}
