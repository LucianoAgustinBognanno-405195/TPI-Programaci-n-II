using Microsoft.EntityFrameworkCore;
using TrabajoPracticoIntegrador___Programación_2.Models;
using TrabajoPracticoIntegrador___Programación_2.Repositories.Interfaces;

namespace TrabajoPracticoIntegrador___Programación_2.Repositories
{
    public class EmpleadoRepository : IEmpleadosRepository
    {
        private readonly CecchettoLogicaContext _context;

        public EmpleadoRepository(CecchettoLogicaContext contex)
        {
            this._context = contex;

        }
        public async Task<List<Empleados>> GetEmpleadoAll() // 1
        {
            List<Empleados> lst = await _context.Empleados.ToListAsync();
            return lst;
        }

        public async Task<Empleados?> GetEmpleadoById(int idEmpl) //2
        {
            if (idEmpl <= 0)
            {
                throw new Exception("El Id debe de ser mayor a 0");
            }
            Empleados? empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == idEmpl);

            if (empleado == null)
            {
                throw new Exception("No existe ese empleado");
            }

            return empleado;
        }
        public async Task<List<Sucursales>> GetSucursales() //3
        {
            List<Sucursales> lst = await _context.Sucursales.ToListAsync();
            return lst;
        }
        public async Task<List<Documentos>> GetDocumentos()
        {
            List<Documentos> lst = await _context.Documentos.ToListAsync();
            return lst;
        }
        public async Task<List<Ciudades>> GetCiudades()
        {
            List<Ciudades> lst = await _context.Ciudades.ToListAsync();
            return lst;
        }
        public async Task<List<Barrios>> GetBarrios(string Ciudad)
        {
            List<Barrios> lst = await _context.Barrios.Where(b => Ciudad == b.IdCiudadNavigation.Ciudad).ToListAsync();
            return lst;
        }
        public async Task<bool> NuevEmpleado(Empleados nuevEmpl) //4
        {
            await _context.Empleados.AddAsync(nuevEmpl);

            int filas = await _context.SaveChangesAsync();
            if (filas == 0)
            {
                throw new Exception("Error a la hora de cargar un nuevo empleado");
            }
            return true;

        }
        public async Task<bool> UpdtEmpleadoDyT(int idEmpl, Empleados nuevEmpl) // 5
        {
            Empleados? empleados = await _context.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == idEmpl);

            Direcciones direcciones = nuevEmpl.IdDireccionNavigation;
            _context.Direcciones.Add(direcciones);

            empleados.IdDireccionNavigation = direcciones;
            empleados.IdTipoEmpleado = nuevEmpl.IdTipoEmpleado;

            int filas = await _context.SaveChangesAsync();

            if (filas == 0)
            {
                throw new Exception("No se pudo actualizar");
            }
            return true;
        }

        public Task<bool> BajaLogicaEmpleado(int idEmpl) // 6
        {
            throw new NotImplementedException();
        }
    }
}
