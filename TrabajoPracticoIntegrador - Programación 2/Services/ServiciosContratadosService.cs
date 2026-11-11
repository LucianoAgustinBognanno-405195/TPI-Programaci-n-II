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
        public async Task<List<Servicios>> GetSCByCliente(int docCliente)
        {
            List<Servicios> lst = await _repo.GetSCByCliente(docCliente);

            if (lst != null && lst.Count > 0)
            {
                return (lst);
            }
            else
            {
                throw new Exception("No existen servicios contratados por ese cliente especificado.");
            }
        }


        //5. GetServicioContratadoByDNIEmpleado
        public async Task<List<Servicios>> GetSCByEmpleado(int docEmpleado)
        {
            List<Servicios> lst = await _repo.GetSCByEmpleado(docEmpleado);

            if (lst != null && lst.Count > 0)
            {
                return lst;
            }
            else
            {
                throw new Exception("No existen servicios contratados asociados a este empleado.");
            }
        }


        //6. GetServicioContratadoBySucursal
        public async Task<List<Servicios>> GetSCBySucursal(string nombreSucursal)
        {
            List<Servicios> lst = await _repo.GetSCBySucursal(nombreSucursal);

            if (lst != null && lst.Count > 0)
            {
                return lst;
            }
            else
            {
                throw new Exception("No existen servicios contratados asociados a esa sucursal.");
            }
        }


        //7. GetServicioContratadoByEstado
        public Task<List<Servicios>> GetSCByEstado(string estado)
        {
            throw new NotImplementedException();
        }


        //8. GetDetalleServicioContratadoByIdServicioContratado
        public Task<List<DetallesServicio>> GetDSCByIdSC(int idSC)
        {
            throw new NotImplementedException();
        }



        //POST
        //9. NuevoServicio(con detalle)
        public Task<bool> NuevoSC(Servicios nuevSC)
        {
            throw new NotImplementedException();
        }



        //PUT
        //10. ModificarExistente
        public Task<Servicios> UpdateSC(int idSC, Servicios updtSC)
        {
            throw new NotImplementedException();
        }


        //11. CambioEstado
        public Task<bool> CambioEstadoSC(int idSC, string nuevEstado)
        {
            throw new NotImplementedException();
        }

    }
}
