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
    public record GetAllAutoresQuery(): IRequest<List<AutorDto>>;

    public class GetAllAutoresHandler : IRequestHandler<GetAllAutoresQuery , List<AutorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAutoresHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
        public async Task<List<AutorDto>> Handle(GetAllAutoresQuery request , CancellationToken cancellationToken)
        {
            var autores = await _unitOfWork.Autores.GetAllAsync();
            if(autores.Count == 0 )
            {
                throw new Exception("No hay libro registrado");
            }
            return _mapper.Map<List<AutorDto>>(autores);
        }
    }
}
