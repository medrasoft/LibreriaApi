using FluentValidation;
using Libreria.Application.Features.Autores.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Features.Autores.Validators
{
    public class UpdateAutoresValidator : AbstractValidator<UpdateAutorCommand>
    {
        public UpdateAutoresValidator()
        {
            RuleFor(x => x.Autor.AutorId)
                .GreaterThan(0).WithMessage("El ID del libro debe ser mayor que cero");

            RuleFor(x => x.Autor.Nombre)
               .NotEmpty().WithMessage("El nombre es obligatorio")
               .MaximumLength(200).WithMessage("El nombre no puede superar los 200 caracteres");

            RuleFor(x => x.Autor.Nacionalidad)
                .NotEmpty().WithMessage("La Nacionalidad es obligatoria");
        }
    }
}
