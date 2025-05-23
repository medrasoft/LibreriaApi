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
    public class AutoresRepository : IAutorRepository
    {
        private readonly LibreriaDbContext _context;
        public AutoresRepository(LibreriaDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Autores libro)
        {
             await _context.Autores.AddAsync(libro);
        }

        public async Task<List<Autores>> GetAllAsync()
        {
           return await _context.Autores.ToListAsync();
        }

        public async Task<Autores> GetByIdAsync(int id)
        {
            return await _context.Autores.FirstOrDefaultAsync(m=>m.AutorId==id);
        }

        public void Remove(Autores libro)
        {
           _context.Autores.Remove(libro);
        }
    }
}
