using Libreria.Domain.Entidades;
using Libreria.Domain.Interfaces;
using Libreria.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Infrastructure.Repository
{
    public class PrestamosRepository : IPrestamosRepository
    {
        private readonly LibreriaDbContext _context;

        public PrestamosRepository(LibreriaDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Prestamos prestamo)
        {
           await _context.Prestamos.AddAsync(prestamo);
        }

        public async Task<List<Prestamos>> GetAllAsync()
        {
            return await _context.Prestamos.ToListAsync();
        }
        public async Task<Prestamos> GetByIdAsync(int id)
        {
           return await _context.Prestamos.FirstOrDefaultAsync(m=>m.PrestamoId==id);
        }

        public async Task<List<Prestamos>> GetPrestamosNoDevueltos()
        {
            return await _context.Prestamos
                            .Where(p => p.FechaDevolucion == null)
                            .Include(p => p.Libros)
                            .ThenInclude(l => l.Autor)   
                            .ToListAsync();
        }

        public void Remove(Prestamos prestamo)
        {
            _context.Remove(prestamo);
        }
    }
}
