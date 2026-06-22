using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.Experiences.Queries.GetExperiences;
using MediatR;
namespace MyPortfolio.API.Controllers.Public
{
    [ApiController]
    [Route("api/experiences")]
    public class ExperienceController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await mediator.Send(new GetExperiencesQuery());
            return Ok(result);
        }
    }
}
