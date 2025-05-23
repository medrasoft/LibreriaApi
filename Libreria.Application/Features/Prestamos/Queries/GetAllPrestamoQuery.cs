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
    public record GetAllPrestamoQuery(): IRequest<List<PrestamosDto>>;

    public class GetAllPrestamoHandler : IRequestHandler<GetAllPrestamoQuery , List<PrestamosDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPrestamoHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
        public async Task<List<PrestamosDto>> Handle(GetAllPrestamoQuery request , CancellationToken cancellationToken)
        {
            var pretamos = await _unitOfWork.Prestamos.GetAllAsync();
            if( pretamos.Count == 0 )
            {
                throw new Exception("No hay prestamos registrado");
            }
            return _mapper.Map<List<PrestamosDto>>(pretamos);
        }
    }
}
