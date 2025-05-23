using Libreria.Application.Features.Usuario.Commands;
using Libreria.Application.Features.Usuario;
using Libreria.Application.Models;
using Libreria.Domain.Entidades;
using Libreria.Domain.Interfaces;
using Libreria.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace LibreriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtService _jwtService;
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(IMediator mediator, IJwtService jwtService , IUsuarioRepository usuarioRepository)
        {
            _mediator = mediator;
            _jwtService = jwtService;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _usuarioRepository.GetByUsernameAsync(request.Username);

            if ( usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password , usuario.PasswordHash) )
                return Unauthorized(new { mensaje = "Credenciales inválidas" });

            var token = _jwtService.GenerateToken(usuario.Username , usuario.Rol);

            return Ok(new { token });
        }


        [HttpPost("registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar([FromBody] UsuarioDto dto)
        {
            try
            {
                var mensaje = await _mediator.Send(new RegistrarUsuarioCommand(dto));
                return Ok(new { mensaje });
            }
            catch ( Exception ex )
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
