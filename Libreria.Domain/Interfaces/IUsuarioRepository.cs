using Libreria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AddAsync(Usuarios usuario);
        Task<Usuarios> GetByUsernameAsync(string username);
    }
}
