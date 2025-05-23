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


namespace Libreria.Application.Features.Libros.Commands
{
    public record CrearLibroCommand(LibroDto Libro) : IRequest<int>;

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
