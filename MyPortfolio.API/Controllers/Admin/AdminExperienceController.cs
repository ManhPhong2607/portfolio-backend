using Microsoft.AspNetCore.Mvc;
using MediatR;
using MyPortfolio.Application.Features.Experiences.Commands.CreateExperience;
using Microsoft.AspNetCore.Authorization;
using MyPortfolio.Application.Features.Experiences.Commands.UpdateExperience;
using MyPortfolio.Application.Features.Experiences.Commands.DeleteExperience;
using MyPortfolio.Application.Features.Skills.Commands.ReorderSkills;
namespace MyPortfolio.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/experiences")]
    [Authorize]
    public class AdminExperienceController(ISender mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExperienceCommand command, CancellationToken ct)
        {
            var id = await mediator.Send(command,ct);
            return Created(string.Empty, new { id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExperienceCommand command, CancellationToken ct)
        {
            await mediator.Send(command with { Id =id}, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await mediator.Send(new DeleteExperienceCommand(id), ct);
            return NoContent();
        }

        [HttpPatch("reorder")]
        public async Task<IActionResult> Reorder([FromBody] ReorderSkillsCommand command, CancellationToken ct)
        {
            await mediator.Send(command, ct);
            return NoContent();
        }
    }
    
}
