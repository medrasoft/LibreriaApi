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

namespace Libreria.Application.Features.Autores.Commands
{
    public record CrearAutorCommand(AutorDto Autor) : IRequest<int>;

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
            var libro = _mapper.Map<Libreria.Domain.Entidades.Autores>(request.Autor);

            var autor=await _unitOfWork.Autores.GetByIdAsync(libro.AutorId);
            if (autor == null)
            {
                throw new Exception("Autor no encontrado");
            }
            await _unitOfWork.Autores.AddAsync(autor);
            await _unitOfWork.SaveChangesAsync();
            return libro.AutorId;

        }
    }

}
