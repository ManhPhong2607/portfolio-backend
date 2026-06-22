using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.Skills.Commands.DeleteSkill;
using MyPortfolio.Application.Features.Technologies.Commands.CreateTechnology;
using MyPortfolio.Application.Features.Technologies.Commands.DeleteTechnology;
using MyPortfolio.Application.Features.Technologies.Commands.UpdateTechnology;
using MyPortfolio.Application.Features.Technologies.Queries.GetTechnologies;

namespace MyPortfolio.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/technologies")]
    [Authorize]
    public class AdminTechnologyController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await mediator.Send(new GetTechnologiesQuery(), ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateTechnologyCommand command, CancellationToken ct)
        {
            var id = await mediator.Send(command, ct);
            return CreatedAtAction(nameof(Create), new { id }, new { id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateTechnologyCommand command, CancellationToken ct)
        {
            await mediator.Send(command with { Id = id }, ct);
            return NoContent();
        }

        //[HttpDelete("{id:guid}")]
        //public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        //{
        //    await mediator.Send(new DeleteTechnologyCommand(id), ct);
        //    return NoContent();
        //}

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            [FromQuery] bool force = false,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new DeleteTechnologyCommand(id, force), ct);

            if (!result.Deleted)
                return Conflict(new { result.WasInUse, result.Message });

            return Ok(new { result.Deleted, result.WasInUse, result.Message });
        }
    }
}
