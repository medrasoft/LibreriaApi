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
    public record GetLibrosAntesDe2000Async(): IRequest<List<LibroDto>>;

    public class GetAllLibroHandler : IRequestHandler<GetLibrosAntesDe2000Async , List<LibroDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllLibroHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
        public async Task<List<LibroDto>> Handle(GetLibrosAntesDe2000Async request , CancellationToken cancellationToken)
        {
            var libros = await _unitOfWork.Libros.GetLibrosAntesDe2000Async();
            if(libros.Count == 0)
            {
                throw new Exception("No hay libro registrado");
            }
            return _mapper.Map<List<LibroDto>>(libros);
        }
    }
}
