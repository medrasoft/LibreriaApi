using Libreria.Application.Features.Libros.Queries;
using Libreria.Application.Features.Prestamos.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriaController : ControllerBase
    {

        private readonly IMediator _mediator;

        public LibreriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("antes-de-2000")]
        public async Task<IActionResult> GetLibrosAntesDe2000()
        {
            try
            {
                var libros = await _mediator.Send(new GetLibrosAntesDe2000Async());
                return Ok(libros);
            }
            catch ( Exception ex )
            {
                return StatusCode(500 , new { error = "Ocurrió un error inesperado." , detalle = ex.Message });
            }
        }

        [HttpGet("no-devueltos")]
        public async Task<IActionResult> GetNoDevueltos()
          {
            try
            {
                var result = await _mediator.Send(new GetAllPrestamoNoDevueltosQuery());
                return Ok(result);
            }
            catch ( Exception ex )
            {
                return StatusCode(500 , new { error = "Error interno" , detalle = ex.Message });
            }
        }
    }
}
