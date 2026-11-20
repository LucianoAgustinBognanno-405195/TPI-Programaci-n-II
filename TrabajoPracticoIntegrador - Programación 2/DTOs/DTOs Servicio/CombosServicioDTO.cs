namespace TrabajoPracticoIntegrador___Programación_2.DTOs
{
    public class ClienteComboDTO
    {
        public int IdCliente { get; set; }
        public string Cliente { get; set; }

        public int? Documento { get; set; }
    }

    public class TipoServicioComboDTO
    {
        public int IdTipoServicio { get; set; }
        public string Descripcion { get; set; }
    }

    public class BateriaComboDTO
    {
        public int IdBateria { get; set; }
        public string Descripcion { get; set; }
    }

    public class VehiculoComboDTO
    {
        public int IdVehiculo { get; set; }
        public string Descripcion { get; set; }
    }
}
