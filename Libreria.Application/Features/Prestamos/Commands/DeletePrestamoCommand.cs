using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Libreria.Application.Features.Prestamos.Commands
{
    public record DeletePrestamoCommand(int Id):IRequest<string>;

    public class DeletePrestamoHandler : IRequestHandler<DeletePrestamoCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public DeletePrestamoHandler(IUnitOfWork unitOfWork , IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<string> Handle(DeletePrestamoCommand request , CancellationToken cancellationToken)
        {
           var prestamo=await _unitOfWork.Prestamos.GetByIdAsync(request.Id);
            if ( prestamo == null )
            {
                throw new Exception("Prestamo no encontrado");
            }
            _unitOfWork.Prestamos.Remove(prestamo);
            await _unitOfWork.SaveChangesAsync();

            return "Préstamo eliminado exitosamente.";

        }
    }
}
