using AutoMapper;
using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Features.Prestamos.Queries
{
    public record GetAllPrestamoNoDevueltosQuery(): IRequest<List<PrestamosNoDevueltosDto>>;

    public class GetAllPrestamoNoDevuelto : IRequestHandler<GetAllPrestamoNoDevueltosQuery , List<PrestamosNoDevueltosDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPrestamoNoDevuelto(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
        public async Task<List<PrestamosNoDevueltosDto>> Handle(GetAllPrestamoNoDevueltosQuery request , CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Prestamos.GetPrestamosNoDevueltos();
            var prest=result.Select(p=>new PrestamosNoDevueltosDto
            {
                LibroId = p.Libros.LibroId ,
                Titulo = p.Libros.Titulo ,
                AutorId = p.Libros.Autor.AutorId ,
                Nombre = p.Libros.Autor.Nombre

            }).ToList();

            if( prest.Count == 0 )
            {
                throw new Exception("No hay prestamos registrado");
            }
            return prest;
        }
    }
}
