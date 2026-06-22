using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.Tags.Queries.GetTags;
using MediatR;
namespace MyPortfolio.API.Controllers.Public
{
    [ApiController]
    [Route("api/tags")]
    public class TagController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await mediator.Send(new GetTagsQuery());
            return Ok(result);
        }
    }
}
