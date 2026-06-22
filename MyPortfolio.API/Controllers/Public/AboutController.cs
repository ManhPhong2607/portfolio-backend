using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.About.Queries.GetAboutProfile;

namespace MyPortfolio.API.Controllers.Public
{
    [ApiController]
    [Route("api/about")]
    public class AboutController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await mediator.Send(new GetAboutProfileQuery(), ct);
            return Ok(result);
        }

        //public async Task<IActionResult> Get(GetAboutProfileQuery query, CancellationToken ct)
        //{
        //    var result = await mediator.Send(query, ct);
        //    return Ok(result);
        //}
    }
}
