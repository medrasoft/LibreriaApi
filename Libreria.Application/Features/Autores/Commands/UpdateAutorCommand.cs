using AutoMapper;
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
