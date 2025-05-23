using AutoMapper;
using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria.Application.Features.Autores;
using Libreria.Domain.Entidades;
using FluentValidation;

namespace Libreria.Application.Features.Autores.Commands
{
    public record CrearAutorCommand(AutorDto Autor) : IRequest<int>;

    public class CrearAutorValidator : AbstractValidator<AutorDto>
    {
        public CrearAutorValidator()
        {
            RuleFor(x => x.Nombre)
               .NotEmpty().WithMessage("El nombre es obligatorio")
               .MaximumLength(200).WithMessage("El nombre no puede superar los 200 caracteres");

            RuleFor(x => x.Nacionalidad)
                .NotEmpty().WithMessage("La Nacionalidad es obligatoria");
        }
    }

    public class CrearAutoroHandler : IRequestHandler<CrearAutorCommand , int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CrearAutoroHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
        public async Task<int> Handle(CrearAutorCommand request , CancellationToken cancellationToken)
        {
            var autor = _mapper.Map<Libreria.Domain.Entidades.Autores>(request.Autor);
            await _unitOfWork.Autores.AddAsync(autor);
            await _unitOfWork.SaveChangesAsync();
            return autor.AutorId;

        }
    }

}
