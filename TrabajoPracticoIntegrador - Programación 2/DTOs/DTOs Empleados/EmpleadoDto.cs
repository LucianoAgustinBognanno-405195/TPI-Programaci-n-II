using TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Empleados;

namespace TrabajoPracticoIntegrador___Programación_2.DTOs
{
    public class EmpleadoDto
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DocumentoDto Documento { get; set; }

        public DireccionDto Direccion { get; set; }

        public int? IdSucursal { get; set; }

        public int? IdTipoEmpleado { get; set; }

        public string Usuario { get; set; }

        public string Contrasenia { get; set; }

       
    }
}
