using Libreria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<List<Autores>> GetAllAsync();
        Task<Autores> GetByIdAsync(int id);
        Task AddAsync(Autores autor);
        void Remove(Autores autor);
    }
}
