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
    public record GetPrestamoByIdQuery(int id) : IRequest<PrestamosDto>;

    public class GetPrestamoByIdHandler : IRequestHandler<GetPrestamoByIdQuery , PrestamosDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPrestamoByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<PrestamosDto> Handle(GetPrestamoByIdQuery request , CancellationToken cancellationToken)
        {
            var prestamo = await _unitOfWork.Prestamos.GetByIdAsync(request.id);
            if ( prestamo == null )
            {
                throw new Exception("Libro no encontrado");
            }
            return  _mapper.Map<PrestamosDto>(prestamo);
        }
    }

}
