﻿using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Libreria.Application.Features.Libros;
using Libreria.Application.Features.Libros.Commands;
using Libreria.Application.Features.Libros.Queries;
using FluentValidation;
using Libreria.Application.Features.Autores.Commands;

namespace Libreria.Application.Features.Libros.Commands
{
    public record DeleteLibroCommand(int Id):IRequest;

    public class DeleteLibroValidator : AbstractValidator<DeleteAutorCommand>
    {
        public DeleteLibroValidator()
        {
            RuleFor(x => x.Id)
               .GreaterThan(0).WithMessage("El ID del libro debe ser mayor que cero");
        }
    }

    public class DeleteLibroHandler : IRequestHandler<DeleteLibroCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public DeleteLibroHandler(IUnitOfWork unitOfWork , IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task Handle(DeleteLibroCommand request , CancellationToken cancellationToken)
        {
           var libro=await _unitOfWork.Libros.GetByIdAsync(request.Id);
            if (libro == null )
            {
                throw new Exception("Libro nmo encontrado");
            }
            _unitOfWork.Libros.Remove(libro);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
