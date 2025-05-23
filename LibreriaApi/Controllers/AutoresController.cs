using Libreria.Application.Features.Autores;
using Libreria.Application.Features.Autores.Commands;
using Libreria.Application.Features.Autores.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Autores = await _mediator.Send(new GetAllAutoresQuery());

            return Ok(Autores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrestamoById(int id)
        {
            var result = await _mediator.Send(new  GetAutoresByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Crear([FromBody] AutorDto dto)
        {
            var id = await _mediator.Send(new CrearAutorCommand(dto));
            return CreatedAtAction(nameof(GetAll) , new { id } , null);
        }

        [HttpPut]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id , [FromBody] AutorDto dto)
        {
            await _mediator.Send(new UpdateAutorCommand(id , dto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _mediator.Send(new DeleteAutorCommand(Id));
            return NoContent();
        }
    }
}
