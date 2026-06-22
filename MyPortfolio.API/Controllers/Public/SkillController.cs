using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.Skills.Queries.GetSkills;

namespace MyPortfolio.API.Controllers.Public
{
    [ApiController]
    [Route("api/skills")]
    public class SkillController(ISender mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await mediator.Send(new GetSkillsQuery());
            return Ok(result);
        }
            
    }
}
