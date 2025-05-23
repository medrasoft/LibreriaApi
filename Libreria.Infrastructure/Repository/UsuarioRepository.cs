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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LibreriaDbContext _context;
        public UsuarioRepository(LibreriaDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Usuarios usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }

        public async Task<Usuarios> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(m => m.Username == username);
        }
    }
}
