using AutoMapper;
using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria.Application.Features.Libros.Commands;
using Libreria.Application.Features.Libros.Queries;
using Libreria.Application.Features.Libros;

namespace Libreria.Application.Features.Libros.Queries
{
    public record GetLibroByIdQuery(int id) : IRequest<LibroDto>;

    public class GetLibroByIdHandler : IRequestHandler<GetLibroByIdQuery , LibroDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLibroByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<LibroDto> Handle(GetLibroByIdQuery request , CancellationToken cancellationToken)
        {
            var libro = await _unitOfWork.Libros.GetByIdAsync(request.id);
            if ( libro == null )
            {
                throw new Exception("Libro no encontrado");
            }
            return  _mapper.Map<LibroDto>(libro);
        }
    }

}
