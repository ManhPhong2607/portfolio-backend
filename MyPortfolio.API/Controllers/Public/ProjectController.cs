using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.Projects.Queries.GetProjectList;
using MyPortfolio.Application.Features.Projects.Queries.GetProjectBySlug;
namespace MyPortfolio.API.Controllers.Public
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task <IActionResult> GetList([FromQuery] GetProjectListQuery query, CancellationToken ct)
        {
            var result = await mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpGet("{Slug}")]
        public async Task<IActionResult> GetBySlug(string slug, CancellationToken ct)
        {
            var result = await mediator.Send(new GetProjectBySlugQuery(slug), ct);
            return Ok(result);
        }
    }
}
