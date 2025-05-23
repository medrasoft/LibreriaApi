using Libreria.Domain.Entidades;
using Libreria.Domain.Interfaces;
using Libreria.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Infrastructure.Repository
{
    public class LibroRepository : ILibroRepository
    {
        private readonly LibreriaDbContext _context;
        public LibroRepository(LibreriaDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Libros libro)
        {
             await _context.Libros.AddAsync(libro);
        }

        public void Remove(Libros libro)
        {
           _context.Libros.Remove(libro);
        }

        public async Task<List<Libros>> GetAll()
        {
            return await _context.Libros.ToListAsync();
        }

        public async Task<List<Libros>> GetAllAsync()
        {
            return await _context.Libros.ToListAsync();
        }

        public async Task<Libros> GetByIdAsync(int id)
        {
            return await _context.Libros.FirstOrDefaultAsync(m=>m.LibroId == id);
        }

        public async Task<List<Libros>> GetLibrosAntesDe2000Async()
        {
            return await _context.Libros.Where(l => l.AnoPublicacion < 2000).ToListAsync();
        }
    }
}
