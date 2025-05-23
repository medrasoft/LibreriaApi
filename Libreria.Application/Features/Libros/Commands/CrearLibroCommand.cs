using AutoMapper;
using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria.Application.Features.Libros;
using Libreria.Domain.Entidades;
using Libreria.Application.Features.Libros.Commands;
using Libreria.Application.Features.Libros.Queries;
using FluentValidation;

namespace Libreria.Application.Features.Libros.Commands
{
    public record CrearLibroCommand(LibroDto Libro) : IRequest<int>;

    public class CrearLibroValidator : AbstractValidator<CrearLibroCommand>
    {
        public CrearLibroValidator()
        {
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
    public class CrearLibroHandler : IRequestHandler<CrearLibroCommand , int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CrearLibroHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
        public async Task<int> Handle(CrearLibroCommand request , CancellationToken cancellationToken)
        {
            var libro = _mapper.Map<Libreria.Domain.Entidades.Libros>(request.Libro);

           var autor=await _unitOfWork.Autores.GetByIdAsync(libro.AutorId);
            if (autor == null)
            {
                throw new Exception("autor no encontrado");
            }
            await _unitOfWork.Libros.AddAsync(libro);
            await _unitOfWork.SaveChangesAsync();
            return libro.AutorId;

        }
    }

}
