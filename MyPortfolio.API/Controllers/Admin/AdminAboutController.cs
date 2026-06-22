using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.About.Commands.UpdateAboutProfile;
using Microsoft.AspNetCore.Authorization;
using MediatR;
namespace MyPortfolio.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/about")]
    [Authorize]
    public class AdminAboutController(ISender mediator) : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProfileCommand command, CancellationToken ct)
        {
            await mediator.Send(command, ct);
            return NoContent();
        }      
    }
}
