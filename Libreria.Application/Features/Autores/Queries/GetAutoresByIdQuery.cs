using AutoMapper;
using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Features.Autores.Queries
{
    public record GetAutoresByIdQuery(int id) : IRequest<AutorDto>;

    public class GetAutoresByIdHandler : IRequestHandler<GetAutoresByIdQuery, AutorDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAutoresByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<AutorDto> Handle(GetAutoresByIdQuery request , CancellationToken cancellationToken)
        {
            var autor = await _unitOfWork.Autores.GetByIdAsync(request.id);
            if ( autor == null )
            {
                throw new Exception("Libro no encontrado");
            }
            return  _mapper.Map<AutorDto>(autor);
        }
    }

}
