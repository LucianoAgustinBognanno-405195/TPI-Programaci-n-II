using TrabajoPracticoIntegrador___Programación_2.Models;
using System.ComponentModel.DataAnnotations;

namespace TrabajoPracticoIntegrador___Programación_2.DTOs
{
    public class CrearDetalleServicioDto
    {
        [Required(ErrorMessage = "La batería es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La batería no es válida.")]
        public int IdBateria { get; set; }

        [Required(ErrorMessage = "El vehículo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El vehículo no es válido.")]
        public int IdVehiculo { get; set; }
    }

    public class ServicioCreateDTO
    {
        public DateOnly? FechaServicio { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "El cliente no es válido.")]
        public int IdCliente { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "El empleado no es válido.")]
        public int IdEmpleado { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "La sucursal no es válida.")]
        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(50, ErrorMessage = "La descripción puede tener hasta 50 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        [Required, Range(1, int.MaxValue, ErrorMessage = "El tipo de servicio no es válido.")]
        public int IdTipoServicio { get; set; }

        // La lista de hojas adjuntas. Puede venir vacía (tus "posibles detalles")

        [MaxLength(20, ErrorMessage = "Un servicio puede tener hasta 20 detalles.")]
        public List<CrearDetalleServicioDto> Detalles { get; set; } = new();
    }
}
