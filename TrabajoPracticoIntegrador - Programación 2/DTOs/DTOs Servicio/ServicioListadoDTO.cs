namespace TrabajoPracticoIntegrador___Programación_2.DTOs
{
    public class ServicioListadoDTO
    {
        public int IdServicio { get; set; }
        public DateOnly Fecha { get; set; }
        public string Cliente { get; set; }
        public string Empleado { get; set; }
        public string Sucursal { get; set; }
        public string Descripcion { get; set; }
        public string TipoServicio { get; set; }
        public string Estado { get; set; }
    }
}
