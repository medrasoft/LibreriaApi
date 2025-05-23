using AutoMapper;
using FluentValidation;
using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Libreria.Application.Features.Autores.Commands
{
    public  record UpdateAutorCommand(int Id, AutorDto Autor): IRequest;

    public class UpdateAutoresValidator : AbstractValidator<UpdateAutorCommand>
    {
        public UpdateAutoresValidator()
        {
            RuleFor(x => x.Id)
               .GreaterThan(0).WithMessage("El ID del libro debe ser mayor que cero");

            RuleFor(x => x.Autor.AutorId)
                .GreaterThan(0).WithMessage("El ID del libro debe ser mayor que cero");

            RuleFor(x => x.Autor.Nombre)
               .NotEmpty().WithMessage("El nombre es obligatorio")
               .MaximumLength(200).WithMessage("El nombre no puede superar los 200 caracteres");

            RuleFor(x => x.Autor.Nacionalidad)
                .NotEmpty().WithMessage("La Nacionalidad es obligatoria");
        }
    }
    public class UpdateAutorHandler : IRequestHandler<UpdateAutorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateAutorHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAutorCommand request , CancellationToken cancellationToken)
        {
            var AutorExistente = await _unitOfWork.Autores.GetByIdAsync(request.Id);
            if (AutorExistente == null)
            {
                throw new Exception("Autor no encontrado");
            }

            _mapper.Map(request.Autor, AutorExistente);
            await _unitOfWork.SaveChangesAsync();
           
        }
    }

}
