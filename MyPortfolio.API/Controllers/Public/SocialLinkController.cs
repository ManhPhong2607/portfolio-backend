using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.SocialLinks.Queries.GetSocialLinks;

namespace MyPortfolio.API.Controllers.Public
{
    [ApiController]
    [Route("api/social-links")]
    public class SocialLinkController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetVisible(CancellationToken ct)
        {
           var result = await mediator.Send(new GetSocialLinksQuery(), ct);
           return Ok(result);
        }
            
    }
}
