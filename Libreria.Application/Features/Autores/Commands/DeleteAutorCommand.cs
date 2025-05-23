using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Libreria.Application.Features.Autores.Commands
{
    public record DeleteAutorCommand(int Id):IRequest;

    public class DeleteAutorHandler : IRequestHandler<DeleteAutorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public DeleteAutorHandler(IUnitOfWork unitOfWork , IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task Handle(DeleteAutorCommand request , CancellationToken cancellationToken)
        {
           var autor=await _unitOfWork.Autores.GetByIdAsync(request.Id);
            if (autor == null )
            {
                throw new Exception("Libro nmo encontrado");
            }
            _unitOfWork.Autores.Remove(autor);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
