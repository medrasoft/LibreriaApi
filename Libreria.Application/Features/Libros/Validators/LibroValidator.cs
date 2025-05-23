using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Libreria.Application.Features.Libros.Commands;

namespace Libreria.Application.Features.Libros.Validators
{
    public class LibroValidator: AbstractValidator<CrearLibroCommand>
    {
        public LibroValidator() {
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
}
