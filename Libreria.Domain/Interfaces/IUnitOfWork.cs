using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IAutorRepository Autores { get; }
        ILibroRepository Libros { get; }
        IPrestamosRepository Prestamos { get; }

        Task<int> SaveChangesAsync();
    }
}
