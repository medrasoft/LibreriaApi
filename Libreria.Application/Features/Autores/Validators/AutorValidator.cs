using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Libreria.Application.Features.Autores.Commands;

namespace Libreria.Application.Features.Autores.Validators
{
    public class AutorValidator: AbstractValidator<CrearAutorCommand>
    {
        public AutorValidator() {
            RuleFor(x => x.Autor.Nombre)
               .NotEmpty().WithMessage("El nombre es obligatorio")
               .MaximumLength(200).WithMessage("El nombre no puede superar los 200 caracteres");

            RuleFor(x => x.Autor.Nacionalidad)
                .NotEmpty().WithMessage("La Nacionalidad es obligatoria");
        }
    }
}
