using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.Skills.Commands.CreateSkill;
using MyPortfolio.Application.Features.Skills.Commands.DeleteSkill;
using MyPortfolio.Application.Features.Skills.Commands.ReorderSkills;
using MyPortfolio.Application.Features.Skills.Commands.UpdateSkill;
namespace MyPortfolio.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/skills")]
    [Authorize]
    public class AdminSkillController(ISender mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateSkillCommand command, CancellationToken ct)
        {
            var id = await mediator.Send(command, ct);
            return Created(string.Empty, new { id });
            // return Ok(new { id });
            //return CreatedAtAction(nameof(Create), new { id }, new { id });
            //return StatusCode(StatusCodes.Status201Created, new { id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateSkillCommand command, CancellationToken ct)
        {
            await mediator.Send(command with { Id = id }, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await mediator.Send(new DeleteSkillCommand(id), ct);
            return NoContent();
        }

        [HttpPatch("reorder")]
        public async Task<IActionResult> Reorder(
            [FromBody] ReorderSkillsCommand command, CancellationToken ct)
        {
            await mediator.Send(command, ct);
            return NoContent();
        }
    }
}
