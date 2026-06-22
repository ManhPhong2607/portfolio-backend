using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MyPortfolio.Application.Features.Tags.Queries.GetTags;
using MyPortfolio.Application.Features.Tags.Commands.CreateTag;
using MyPortfolio.Application.Features.Tags.Commands.UpdateTag;
using MyPortfolio.Application.Features.Skills.Commands.DeleteSkill;
using MyPortfolio.Application.Features.Tags.Commands.DeleteTag;

namespace MyPortfolio.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/tags")]
    [Authorize]
    public class AdminTagController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await mediator.Send(new GetTagsQuery());
            return Ok(result);  
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTagCommand command, CancellationToken ct)
        {
            var id = await mediator.Send(command, ct);
            return Created(string.Empty, new { id }); 
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTagCommand command, CancellationToken ct)
        {
            await mediator.Send(command with { Id = id}, ct);
            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await mediator.Send(new DeleteTagCommand(id), ct);
            return NoContent();
        }

        //public async Task<IActionResult> Delete(
        //Guid id, [FromQuery] bool force = false, CancellationToken ct = default)
        //{
        //    var result = await mediator.Send(new DeleteTagCommand(id, force), ct);

        //    if (!result.Deleted)
        //        return Conflict(new { result.WasInUse, result.Message });

        //    return Ok(new { result.Deleted, result.WasInUse, result.Message });
        //}


    }
}
