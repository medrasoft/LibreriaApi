using AutoMapper;
using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria.Application.Features.Libros;
using Libreria.Domain.Entidades;
using System.Runtime.InteropServices;

namespace Libreria.Application.Features.Prestamos.Commands
{
    public record CrearPrestamoCommand(PrestamosDto prestamoDto) : IRequest<int>;

    public class CrearPrestamoHandler : IRequestHandler<CrearPrestamoCommand , int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CrearPrestamoHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
        public async Task<int> Handle(CrearPrestamoCommand request , CancellationToken cancellationToken)
        {
             var prestamo = _mapper.Map<Libreria.Domain.Entidades.Prestamos>(request.prestamoDto);
             var libro = await _unitOfWork.Libros.GetByIdAsync(prestamo.LibroId);
            // prestamo.FechaDevolucion = null;
             if ( libro == null )
                 throw new Exception("El libro no existe.");

             await _unitOfWork.Prestamos.AddAsync(prestamo);
             await _unitOfWork.SaveChangesAsync();
             return prestamo.PrestamoId;
            
        }
    }

}
