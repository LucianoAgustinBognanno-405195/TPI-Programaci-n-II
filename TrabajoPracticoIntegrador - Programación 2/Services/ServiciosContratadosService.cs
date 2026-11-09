using Microsoft.AspNetCore.Components.Endpoints.Infrastructure;
using TrabajoPracticoIntegrador___Programación_2.Models;
using TrabajoPracticoIntegrador___Programación_2.Repositories.Interfaces;
using TrabajoPracticoIntegrador___Programación_2.Services.Interfaces;

namespace TrabajoPracticoIntegrador___Programación_2.Services
{
    public class ServiciosContratadosService : IServiciosContratadosService
    {
        private readonly IServiciosContratadosRepository _repo;

        public ServiciosContratadosService(IServiciosContratadosRepository repo)
        {
            _repo = repo;
        }

        public Task<bool> CambioEstadoSC(int idSC, string nuevEstado)
        {
            throw new NotImplementedException();
        }

        public Task<List<DetallesServicio>> GetDSCByIdSC(int idSC)
        {
            throw new NotImplementedException();
        }

        public Task<List<Servicio>> GetSCAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Servicio>> GetSCByCliente(int dniCliente)
        {
            throw new NotImplementedException();
        }

        public Task<List<Servicio>> GetSCByEmpleado(int dniEmpleado)
        {
            throw new NotImplementedException();
        }

        public Task<List<Servicio>> GetSCByEstado(string estado)
        {
            throw new NotImplementedException();
        }

        public Task<List<Servicio>> GetSCByFecha(DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public Servicio? GetSCById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Servicio>> GetSCBySucursal(string nombreSucursal)
        {
            throw new NotImplementedException();
        }

        public Task<bool> NuevoSC(Servicio nuevSC)
        {
            throw new NotImplementedException();
        }

        public Task<Servicio> UpdateSC(int idSC, Servicio updtSC)
        {
            throw new NotImplementedException();
        }
    }
}
