using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.SocialLinks.Commands.CreateSocialLink;
using MyPortfolio.Application.Features.SocialLinks.Commands.DeleteSocialLink;
using MyPortfolio.Application.Features.SocialLinks.Commands.ReorderSocialLink;
using MyPortfolio.Application.Features.SocialLinks.Commands.ToogleSocialLink;
using MyPortfolio.Application.Features.SocialLinks.Commands.UpdateSocialLink;
using MyPortfolio.Application.Features.SocialLinks.Queries.GetAllSocialLinks;
using MyPortfolio.Application.Features.SocialLinks.Queries.GetSocialLinks;

namespace MyPortfolio.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/social-links")]
    [Authorize]
    public class AdminSocialLinkController(ISender mediator) : ControllerBase
    {
        //cả sociallink bị ẩn
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await mediator.Send(new GetAllSocialLinksQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSocialLinkCommand command,CancellationToken ct)
        {
            var id = await mediator.Send(command, ct); 
            return Created(string.Empty, new { id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id,[FromBody] UpdateSocialLinkCommand command, CancellationToken ct)
        {
            await mediator.Send(command with { Id = id }, ct);
            return NoContent();
        }

        [HttpPatch("{id:guid}/toggle")]
        public async Task<IActionResult> Toggle(Guid id, CancellationToken ct)
        {
            var isVisible = await mediator.Send(new ToggleSocialLinkCommand(id), ct);
            return Ok(new { isVisible });
        }

        [HttpPatch("reorder")]
        public async Task<IActionResult> Reorder(
        [FromBody] ReorderSocialLinkCommand command, CancellationToken ct)
        {
            await mediator.Send(command, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await mediator.Send(new DeleteSocialLinkCommand(id), ct);
            return NoContent();
        }
    }
}
