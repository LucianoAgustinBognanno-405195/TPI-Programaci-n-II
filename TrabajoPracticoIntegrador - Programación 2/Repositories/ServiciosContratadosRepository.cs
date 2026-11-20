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

        private IQueryable<Servicios> ServiciosConDatos()
        {
            return _dbContext.Servicios
                .Include(s => s.IdClienteNavigation)
                .Include(s => s.IdEmpleadoNavigation)
                .Include(s => s.IdSucursalNavigation)
                .Include(s => s.IdTipoServicioNavigation)
                .Include(s => s.EstadoServicioNavigation)
                .AsNoTracking();
        }
        //Es como una “plantilla de consulta” que ya trae los datos de las otras tablas.
        //Un IQueryable es una consulta armada que todavía no se ejecutó, y se ejecuta recién con el ToListAsync().
        //El AsNoTracking() le dice a EF “esto es solo para leer, no hace falta que vigiles los cambios”, lo que lo hace un poco más rápido.

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
            Servicios? serv = await _dbContext.Servicios
                .Include(s => s.IdClienteNavigation)
                .Include(s => s.IdEmpleadoNavigation)
                .Include(s => s.IdSucursalNavigation)
                .Include(s => s.IdTipoServicioNavigation)
                .Include(s => s.EstadoServicioNavigation)
                .FirstOrDefaultAsync(s => s.IdServicio == id);

            return serv;

            //Si hacés Include de una tabla, nunca devuelvas la entidad al cliente: devolvé un DTO.
            //Los Include son para que el Service pueda armar el DTO. Sin DTO, no uses Include.
        }

        //3. GetServicioContratadoByFecha
        public async Task<List<Servicios>> GetSCByFecha(DateOnly fechaInicio, DateOnly? fechaFin)
        {
            if (fechaFin == null)
            {
                List<Servicios> lst = await ServiciosConDatos().Where(s => s.FechaServicio >= fechaInicio &&
                                                                        s.FechaServicio <= DateOnly.FromDateTime(DateTime.Today)).  //Today es la fecha de tu computadora (Argentina)
                                                                        ToListAsync();

                return lst;
            }
            else
            {
                List<Servicios> lst = await ServiciosConDatos().Where(s => s.FechaServicio >= fechaInicio &&
                                                        s.FechaServicio <= fechaFin).
                                                        ToListAsync();

                return lst;
            }
        }

        //4. GetServicioContratadoByCLiente
        public async Task<List<Servicios>> GetSCByCliente(int docCliente)
        {
            List<Servicios> lst = await ServiciosConDatos().Where(s => s.IdClienteNavigation.IdDocumentoNavigation.Documento == docCliente).ToListAsync();
            //Con esto la lista viene con cliente, empleado, sucursal, etc. cargados, listos para armar el DTO.

            //List<Servicio> lst = null;

            return lst;
        }

        //5. GetServicioContratadoByDNIEmpleado
        public async Task<List<Servicios>> GetSCByEmpleado(int docEmpleado)
        {
            List<Servicios> lst = await ServiciosConDatos().Where(s => s.IdEmpleadoNavigation.IdDocumentoNavigation.Documento == docEmpleado).ToListAsync();

            return lst;

            //no conviene filtrar por Activo, e consulta el historial, y un empleado que ya no trabaja sigue teniendo servicios pasados que alguien puede querer ver.
        }

        //6. GetServicioContratadoBySucursal
        public async Task<List<Servicios>> GetSCBySucursal(string nombreSucursal)
        {
            List<Servicios> lst = await ServiciosConDatos().Where(s => s.IdSucursalNavigation.Sucursal.StartsWith(nombreSucursal)).ToListAsync();
            // SQL Server, con su configuración habitual, no distingue mayúsculas al comparar texto,    //StartsWith busca solo por el comienzo del nombre no lo que contiene

            return lst;
        }

        //7. GetServicioContratadoByEstado
        public async Task<List<Servicios>> GetSCByEstado(string estado)
        {
            List<Servicios> lst = await ServiciosConDatos().Where(s => s.EstadoServicioNavigation.Estado == estado).ToListAsync();

            return lst;
        }

        //8. GetDetalleServicioContratadoByIdServicioContratado
        public async Task<List<DetallesServicio>> GetDSCByIdSC(int idSC)
        {
            List<DetallesServicio> lst = await _dbContext.DetallesServicio.Where(ds => ds.IdServicio == idSC).
                                                                           Include(s => s.IdBateriaNavigation).ThenInclude(d => d.IdTipoBateriaNavigation).
                                                                           Include(s => s.IdBateriaNavigation).ThenInclude(v => v.IdVoltajeNavigation).                                
                                                                           Include(s => s.IdVehiculoNavigation).ThenInclude(d => d.IdTipoVehiculoNavigation).
                                                                           ToListAsync();

            return lst;
        }

        //POST
        //9. NuevoServicio(con detalle)
        public async Task<int> NuevoSC(Servicios servicio)
        {
            await _dbContext.Servicios.AddAsync(servicio);
            await _dbContext.SaveChangesAsync();    // todo o nada (el servicio y sus detalles)
            return servicio.IdServicio;
        }
        // Porque cuando le pasamos una ficha que trae sus hojas adjuntas pegadas, EF las detecta y hace todo junto:
        // Guarda la ficha principal y la base le da un Id(por ejemplo, el 25).
        // Copia ese 25 en el casillero Id_Servicio de cada hoja.
        // Guarda las hojas.

        // ¿Existe el empleado y está activo?
        public async Task<bool> EmpleadoActivoAsync(int idEmpleado)
        {
            return await _dbContext.Empleados.AnyAsync(e => e.IdEmpleado == idEmpleado && e.Activo);    //AnyAsync pregunta “¿hay al menos uno que cumpla esto?
        }

        //PUT
        //10. ModificarExistente
        public async Task<bool> UpdateSC(int idSC, Servicios updtSC)
        {
           
            Servicios? serv = await _dbContext.Servicios
                .FirstOrDefaultAsync(s => s.IdServicio == idSC);

            if (serv == null)
            {
                return false;
            }

            serv.FechaServicio = updtSC.FechaServicio;
            serv.IdCliente = updtSC.IdCliente;
            serv.IdEmpleado = updtSC.IdEmpleado;
            serv.IdSucursal = updtSC.IdSucursal;
            serv.Descripcion = updtSC.Descripcion;
            serv.IdTipoServicio = updtSC.IdTipoServicio;

            foreach (DetallesServicio detalle in updtSC.DetallesServicio)
            {
                serv.DetallesServicio.Add(new DetallesServicio
                {
                    IdBateria = detalle.IdBateria,
                    IdVehiculo = detalle.IdVehiculo
                });
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        //11. CambioEstado
        // Devuelve true si encontro el servicio y lo actualizo, false si no existe
        public async Task<int> CambioEstadoSC(int idSC, int nuevEstado)
        {
            Servicios? serv = await _dbContext.Servicios.FirstOrDefaultAsync(s => s.IdServicio == idSC);

            if (serv == null)
            {
                return 0;
            }

            if (serv.EstadoServicio == 3 || serv.EstadoServicio == 4)
            {
                return 1;
            }

            serv.EstadoServicio = nuevEstado;   // cambiamos la fk, no la navegacion
            await _dbContext.SaveChangesAsync();

            return 2;
        }

        //12. Modificar DetalleServicio
        public async Task<bool> UpdateDSC(int idSC, DetallesServicio detServicio)
        {
            DetallesServicio? det = await _dbContext.DetallesServicio.FirstOrDefaultAsync(d => d.IdServicio == idSC
                                                                                               && d.IdDetalleServicio == detServicio.IdDetalleServicio);
            if (det == null)
            {
                return false;
            }

            det.IdBateria = detServicio.IdBateria;
            det.IdVehiculo = detServicio.IdVehiculo;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // Devuelve el estado del servicio, o null si el servicio no existe
        public async Task<int?> GetEstadoServicioAsync(int idSC)
        {
            return await _dbContext.Servicios
                .Where(s => s.IdServicio == idSC)
                .Select(s => (int?)s.EstadoServicio)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Clientes>> GetClientes()
        {
            return await _dbContext.Clientes
                .Include(c => c.IdDocumentoNavigation)
                .OrderBy(c => c.Apellido)
                .ThenBy(c => c.Nombre)
                .ToListAsync();
        }

        public async Task<List<TiposServicios>> GetTiposServicio()
        {
            return await _dbContext.TiposServicios
                .OrderBy(t => t.Descripcion)
                .ToListAsync();
        }
    
        public async Task<List<Baterias>> GetBaterias()
        {
            return await _dbContext.Baterias
                .Include(b => b.IdMarcaNavigation)
                .Include(b => b.IdTipoBateriaNavigation)
                .Include(b => b.IdVoltajeNavigation)
                .Include(b => b.IdTamanioBateriaNavigation)
                .OrderBy(b => b.IdBateria)
                .ToListAsync();
        }

        public async Task<List<Vehiculos>> GetVehiculos()
        {
            return await _dbContext.Vehiculos
                .Include(v => v.IdTipoVehiculoNavigation)
                .OrderBy(v => v.Descripcion)
                .ToListAsync();
        }
    }
}
