using AutoMapper;
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

namespace Libreria.Application.Features.Libros.Commands
{
    public  record UpdateLibroCommand(int Id, LibroDto Libro): IRequest;

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
