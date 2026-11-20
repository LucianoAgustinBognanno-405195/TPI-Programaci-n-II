using System.ComponentModel.DataAnnotations;

namespace TrabajoPracticoIntegrador___Programación_2.DTOs
{
    public class ServiceUpdatDto
    {
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateOnly? FechaServicio { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El cliente no es válido.")]
        public int IdCliente { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El empleado no es válido.")]
        public int IdEmpleado { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La sucursal no es válida.")]
        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(50, ErrorMessage = "La descripción puede tener hasta 50 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "El tipo de servicio no es válido.")]
        public int IdTipoServicio { get; set; }

        [MaxLength(20, ErrorMessage = "Se pueden agregar hasta 20 detalles.")]
        public List<CrearDetalleServicioDto> Detalles { get; set; } = new();
    }
}
