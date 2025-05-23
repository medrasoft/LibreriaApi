using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Libreria.Application.Features.Libros;
using Libreria.Application.Features.Libros.Queries;
using Libreria.Application.Features.Libros.Commands;

namespace LibreriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
      private readonly IMediator _mediator;

        public LibrosController(IMediator mediator) 
        { 
           _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var libros=await _mediator.Send(new GetLibrosAntesDe2000Async());
           
            return Ok(libros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetLibroById(int id)
        {
            var result=await _mediator.Send(new GetLibroByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Crear([FromBody] LibroDto dto)
        {
            var id=await _mediator.Send(new CrearLibroCommand(dto));
            return CreatedAtAction(nameof(GetAll), new {id }, null);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id , [FromBody] LibroDto dto)
        {
            await _mediator.Send(new UpdateLibroCommand(id , dto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLibroCommand(id));
            return NoContent();
        }
    }
}
