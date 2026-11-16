namespace TrabajoPracticoIntegrador___Programación_2.DTOs.DTOs_Empleados
{
    // Este Dto se creo para mostrar los empleados sin la contraseña en el front
    public class EmpleadoListadoDto
    {
        public int IdEmpleado { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Usuario { get; set; }

        public bool Activo { get; set; }
    }
}
