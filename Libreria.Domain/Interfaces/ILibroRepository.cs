using Libreria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Domain.Interfaces
{
    public interface ILibroRepository
    {
        Task<List<Libros>> GetLibrosAntesDe2000Async();
        Task <Libros> GetByIdAsync(int id);
        Task AddAsync(Libros libro);
        void Remove(Libros libro);
    }
}
