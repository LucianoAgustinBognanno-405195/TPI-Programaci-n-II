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

        


        //GET
        //1. GetServicioContratadoAll
        public async Task<List<Servicios>> GetSCAll()
        {

            List<Servicios> lst = await _repo.GetSCAll();

            if (lst != null && lst.Count > 0)
            {
                return (lst);
            }
            else
            {
                throw new Exception("No existen servicios contratados");
            }
        }

        //2. GetServicioContratadoById
        public async Task<Servicios?> GetSCById(int id)
        {
            Servicios? serv = await _repo.GetSCById(id);

            if (serv != null)
            {
                return (serv);
            }
            else
            {
                throw new Exception("No existe servicio contratado con ese Id");
            }
        }


        //3. GetServicioContratadoByFecha
        public async Task<List<Servicios>> GetSCByFecha(DateTime fechaInicio, DateTime? fechaFin)
        {
            List<Servicios> lst = await _repo.GetSCByFecha(fechaInicio, fechaFin);

            if (lst != null && lst.Count > 0)
            {
                return (lst);
            }
            else
            {
                throw new Exception("No existen servicios contratados a partir de esa fecha.");
            }
        }

        //4. GetServicioContratadoByCLiente
        public async Task<List<Servicios>> GetSCByCliente(int dniCliente)
        {
            List<Servicios> lst = await _repo.GetSCByCliente(dniCliente);

            if (lst != null && lst.Count > 0)
            {
                return (lst);
            }
            else
            {
                throw new Exception("No existen servicios contratados por ese cliente especificado.");
            }
        }




        public Task<List<Servicios>> GetSCByEmpleado(int dniEmpleado)
        {
            throw new NotImplementedException();
        }

        public Task<List<Servicios>> GetSCByEstado(string estado)
        {
            throw new NotImplementedException();
        }

        public Task<List<Servicios>> GetSCBySucursal(string nombreSucursal)
        {
            throw new NotImplementedException();
        }

        public Task<bool> NuevoSC(Servicios nuevSC)
        {
            throw new NotImplementedException();
        }

        public Task<Servicios> UpdateSC(int idSC, Servicios updtSC)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CambioEstadoSC(int idSC, string nuevEstado)
        {
            throw new NotImplementedException();
        }

        
        public Task<List<DetallesServicio>> GetDSCByIdSC(int idSC)
        {
            throw new NotImplementedException();
        }


    }
}
