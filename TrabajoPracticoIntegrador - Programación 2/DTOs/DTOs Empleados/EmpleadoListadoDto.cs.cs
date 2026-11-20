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

        public DocumentoDto Documento { get; set; }

        //NUEVOCAMBIO: datos que usa el formulario de edicion (solo se completan al pedir un empleado por id)
        public int? IdTipoEmpleado { get; set; }
        public string? Ciudad { get; set; }
        public DireccionDto? Direccion { get; set; }
    }
}
