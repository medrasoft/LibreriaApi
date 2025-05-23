using Libreria.Domain.Interfaces;
using Libreria.Infrastructure.Repository;
using Libreria.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibreriaDbContext _context;

        public UnitOfWork(LibreriaDbContext context)
        {
            _context = context;
            Libros = new LibroRepository(_context);
            Autores = new AutoresRepository(_context);
            Prestamos = new PrestamosRepository(_context);
            
        }

        public IAutorRepository Autores { get; }

        public ILibroRepository Libros { get; }

        public IPrestamosRepository Prestamos {get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
