using AutoMapper;
using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Libreria.Application.Features.Prestamos.Commands
{
    public  record UpdatePrestamoCommand(int Id, PrestamoPutDto Prestamo): IRequest;

    public class UpdatePrestamoHandler : IRequestHandler<UpdatePrestamoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdatePrestamoHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdatePrestamoCommand request , CancellationToken cancellationToken)
        {
            var PrestamoExistente = await _unitOfWork.Prestamos.GetByIdAsync(request.Id);
            if ( PrestamoExistente == null)
            {
                throw new Exception("Prestamo no encontrado");
            }

            _mapper.Map(request.Prestamo, PrestamoExistente);
            await _unitOfWork.SaveChangesAsync();
           
        }
    }

}
