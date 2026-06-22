using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.BlogPosts.Commands.IncrementViewCount;
using MyPortfolio.Application.Features.BlogPosts.Queries.GetBlogBySlug;
using MyPortfolio.Application.Features.BlogPosts.Queries.GetBlogList;

namespace MyPortfolio.API.Controllers.Public
{
    [ApiController]
    [Route("api/blogs")]
    public class BlogController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetList(
         [FromQuery] GetBlogListQuery query, CancellationToken ct)
        {
            var result = await mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug, CancellationToken ct)
        {
            var result = await mediator.Send(new GetBlogBySlugQuery(slug), ct);
            return Ok(result);
        }
           
        [HttpPost("{slug}/view")]
        public async Task<IActionResult> IncrementView(string slug, CancellationToken ct)
        {
            await mediator.Send(new IncrementViewCountCommand(slug));
            return Ok();
        }
    }
}
