using Libreria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Domain.Interfaces
{
    public interface IPrestamosRepository
    {
        Task<List<Prestamos>> GetPrestamosNoDevueltos();
        Task<List<Prestamos>> GetAllAsync();
        Task<Prestamos> GetByIdAsync(int id);
        Task AddAsync(Prestamos prestamo);
        void Remove(Prestamos prestamo);
    }
}
