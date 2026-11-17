//NUEVOCAMIO: archivo nuevo. DTOs para llenar los desplegables del formulario de nuevo servicio
namespace TrabajoPracticoIntegrador___Programación_2.DTOs
{
    public class ClienteComboDTO
    {
        public int IdCliente { get; set; }
        public string Cliente { get; set; }   // "Nombre Apellido"
    }

    public class TipoServicioComboDTO
    {
        public int IdTipoServicio { get; set; }
        public string Descripcion { get; set; }
    }
}
