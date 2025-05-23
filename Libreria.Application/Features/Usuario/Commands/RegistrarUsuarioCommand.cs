using Libreria.Domain.Entidades;
using Libreria.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Features.Usuario.Commands
{
    public record RegistrarUsuarioCommand(UsuarioDto Usuario) : IRequest<string>;

    public class RegistrarUsuarioHandler : IRequestHandler<RegistrarUsuarioCommand , string>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrarUsuarioHandler(IUsuarioRepository usuarioRepository , IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(RegistrarUsuarioCommand request , CancellationToken cancellationToken)
        {
            var existe = await _usuarioRepository.GetByUsernameAsync(request.Usuario.Username);
            if ( existe != null )
                throw new Exception("El nombre de usuario ya está en uso.");

            var usuario = new Usuarios
            {
                Username = request.Usuario.Username ,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Usuario.Password) ,
                Rol = request.Usuario.Rol 
            };

            await _usuarioRepository.AddAsync(usuario);
            await _unitOfWork.SaveChangesAsync();

            return "Usuario registrado exitosamente.";
        }
    }

}
