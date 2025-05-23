﻿using AutoMapper;
using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Libreria.Application.Features.Libros;
using Libreria.Application.Features.Libros.Commands;
using Libreria.Application.Features.Libros.Queries;
using FluentValidation;

namespace Libreria.Application.Features.Libros.Commands
{
    public  record UpdateLibroCommand(int Id, LibroDto Libro): IRequest;

    public class UpdateLibroValidator : AbstractValidator<UpdateLibroCommand>
    {
        public UpdateLibroValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID del libro debe ser mayor que cero");

            RuleFor(x => x.Libro.Titulo)
                .NotEmpty().WithMessage("El título es obligatorio")
                .MaximumLength(200).WithMessage("El título no puede superar los 200 caracteres");

            RuleFor(x => x.Libro.AutorId)
                .GreaterThan(0).WithMessage("Debe proporcionar un autor válido");

            RuleFor(x => x.Libro.AnoPublicacion)
                .InclusiveBetween(1500 , DateTime.Now.Year)
                .WithMessage($"El año debe estar entre 1500 y {DateTime.Now.Year}");

            RuleFor(x => x.Libro.Genero)
                .NotEmpty().WithMessage("El género es obligatorio")
                .MaximumLength(100).WithMessage("El género no puede superar los 100 caracteres");
        }
    }

    public class UpdateLibroHandler : IRequestHandler<UpdateLibroCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateLibroHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateLibroCommand request , CancellationToken cancellationToken)
        {
            var libroExistente = await _unitOfWork.Libros.GetByIdAsync(request.Id);
            if (libroExistente == null)
            {
                throw new Exception("Libro no encontrado");
            }

            _mapper.Map(request.Libro, libroExistente);
            await _unitOfWork.SaveChangesAsync();
           
        }
    }

}
