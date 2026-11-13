using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using TrabajoPracticoIntegrador___Programación_2.DTOs;
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
            List<Servicios> lst = await _dbContext.Servicios.Include(s => s.IdClienteNavigation)
                                                      .Include(s => s.IdEmpleadoNavigation)
                                                      .Include(s =>s.IdSucursalNavigation)
                                                      .Include(s => s.IdTipoServicioNavigation)
                                                      .Include(s => s.EstadoServicioNavigation)
                                                      .ToListAsync();

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
        public async Task<List<Servicios>> GetSCByFecha(DateOnly fechaInicio, DateOnly? fechaFin)
        {
            if (fechaFin == null)
            {
                List<Servicios> lst = await _dbContext.Servicios.Where(s => s.FechaServicio >= fechaInicio &&
                                                                        s.FechaServicio <= DateOnly.FromDateTime(DateTime.UtcNow)).
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


        //5. GetServicioContratadoByDNIEmpleado
        public async Task<List<Servicios>> GetSCByEmpleado(int docEmpleado)
        {
            List<Servicios> lst = await _dbContext.Servicios.Where(s => s.IdEmpleadoNavigation.IdDocumentoNavigation.Documento == docEmpleado).ToListAsync();

            return lst;
        }


        //6. GetServicioContratadoBySucursal
        public async Task<List<Servicios>> GetSCBySucursal(string nombreSucursal)
        {
            List<Servicios> lst = await _dbContext.Servicios.Where(s => s.IdSucursalNavigation.Sucursal == nombreSucursal).ToListAsync();

            return lst;
        }


        //7. GetServicioContratadoByEstado
        public async Task<List<Servicios>> GetSCByEstado(string estado)
        {
            List<Servicios> lst = await _dbContext.Servicios.Where(s => s.EstadoServicioNavigation.Estado == estado).ToListAsync();

            return lst;
        }


        //8. GetDetalleServicioContratadoByIdServicioContratado
        public async Task<List<DetallesServicio>> GetDSCByIdSC(int idSC)
        {
            List<DetallesServicio> lst = await _dbContext.DetallesServicio.Where(ds => ds.IdServicioNavigation.IdServicio == idSC).
                                                                           Include(s => s.IdBateriaNavigation).ThenInclude(d => d.IdTipoBateriaNavigation).
                                                                           Include(s => s.IdBateriaNavigation).ThenInclude(v => v.IdVoltajeNavigation).                                
                                                                           Include(s => s.IdServicioNavigation).
                                                                           Include(s => s.IdVehiculoNavigation).ThenInclude(d => d.IdTipoVehiculoNavigation).
                                                                           ToListAsync();

            return lst;
        }



        //POST
        //9. NuevoServicio(con detalle)
        public async Task<bool> NuevoSC(Servicios nuevSC)
        {
            try
            {
                await _dbContext.Servicios.AddAsync(nuevSC);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
            
        }



        //PUT
        //10. ModificarExistente
        public async Task<Servicios> UpdateSC(int idSC, Servicios updtSC)
        {
            throw new NotImplementedException();
        }


        //11. CambioEstado
        public async Task<bool> CambioEstadoSC(int idSC, int nuevEstado)
        {
            Servicios? serv = await _dbContext.Servicios.Include(s => s.EstadoServicioNavigation).FirstOrDefaultAsync(s => s.IdServicio == idSC);

            try
            {
                if (serv != null)
                {
                    serv.EstadoServicioNavigation.IdEstado = nuevEstado;

                    int filas = await _dbContext.SaveChangesAsync();

                    if (filas > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    throw new Exception("No existe el servicio indicado con ese Id.");
                }
            }
            catch (Exception)
            {

                throw new Exception("No se puedo modificar el nuevo estado.");
            }
        }

        //12. Modificar DetalleServicio
        public async Task<DetallesServicio> UpdateDSC(int idSC, DetallesServicio detServicio)
        {
            throw new NotImplementedException();
        }
    }
}
