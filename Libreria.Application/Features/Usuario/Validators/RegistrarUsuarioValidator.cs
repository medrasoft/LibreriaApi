using FluentValidation;
using Libreria.Application.Features.Usuario.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Features.Usuario.Validators
{
    public class RegistrarUsuarioValidator : AbstractValidator<UsuarioDto>
    {
        public RegistrarUsuarioValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es requerido.")
                .MinimumLength(4);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MinimumLength(6);

            RuleFor(x => x.Rol)
            .NotEmpty().WithMessage("El rol es requerido.")
            .Must(rol => rol == "Admin" || rol == "Usuario")
            .WithMessage("El rol debe ser 'Admin' o 'Usuario'");
        }
    }
}
