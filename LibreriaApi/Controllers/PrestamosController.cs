using Libreria.Application.Features.Prestamos;
using Libreria.Application.Features.Prestamos.Commands;
using Libreria.Application.Features.Prestamos.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrestamosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var prestamos = await _mediator.Send(new GetAllPrestamoQuery());

            return Ok(prestamos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrestamoById(int id)
        {
            var result = await _mediator.Send(new GetPrestamoByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Crear([FromBody] PrestamosDto dto)
        {
            var id = await _mediator.Send(new CrearPrestamoCommand(dto));
            return CreatedAtAction(nameof(GetAll) , new { id } , null);
        }

        [HttpPut]
         [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id , [FromBody] PrestamoPutDto dto)
        {
            await _mediator.Send(new UpdatePrestamoCommand(id , dto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePrestamoCommand(id));
            return NoContent();
        }
    }
}
