using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using TrabajoPracticoIntegrador___Programación_2.Models;
using TrabajoPracticoIntegrador___Programación_2.Repositories.Interfaces;

namespace TrabajoPracticoIntegrador___Programación_2.Repositories
{
    public class ServiciosContratadosRepository : IServiciosContratadosRepository
    {
        private readonly CecchettoLogicaContext _dbContext;

        public  ServiciosContratadosRepository(CecchettoLogicaContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET
        //1. GetServicioContratadoAll
        public async Task<List<Servicios>> GetSCAll()
        {
            List<Servicios> lst = await _dbContext.Servicios.ToListAsync();

            //List<Servicio> lst = null;

            return lst;
        }

        //2. GetServicioContratadoById
        public async Task<Servicios?> GetSCById(int id)
        {
            Servicios? serv = await _dbContext.Servicios.FirstOrDefaultAsync(s => s.IdServicio == id);

            return serv;
        }

        //3. GetServicioContratadoByFecha
        public async Task<List<Servicios>> GetSCByFecha(DateTime fechaInicio, DateTime? fechaFin)
        {
            if (fechaFin == null)
            {
                List<Servicios> lst = await _dbContext.Servicios.Where(s => s.FechaServicio >= fechaInicio &&
                                                                        s.FechaServicio <= DateTime.UtcNow).
                                                                        ToListAsync();

                return lst;
            }
            else
            {
                List<Servicios> lst = await _dbContext.Servicios.Where(s => s.FechaServicio >= fechaInicio &&
                                                        s.FechaServicio <= fechaFin).
                                                        ToListAsync();

                return lst;
            }
        }

        //4. GetServicioContratadoByCLiente
        public async Task<List<Servicios>> GetSCByCliente(int docCliente)
        {
            List<Servicios> lst = await _dbContext.Servicios.Where(s => s.IdClienteNavigation.IdDocumentoNavigation.Documento == docCliente).ToListAsync();

            //List<Servicio> lst = null;

            return lst;
        }



        public async Task<List<DetallesServicio>> GetDSCByIdSC(int idSC)
        {
            throw new NotImplementedException();
        }


        public Task<bool> CambioEstadoSC(int idSC, string nuevEstado)
        {
            throw new NotImplementedException();
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







    }
}
