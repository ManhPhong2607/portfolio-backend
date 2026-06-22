using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.Technologies.Queries.GetTechnologies;

namespace MyPortfolio.API.Controllers.Public
{
    [ApiController]
    [Route("api/technologies")]
    public class TechnologyController(ISender mediator) : ControllerBase
    {   
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await mediator.Send(new GetTechnologiesQuery(), ct);
            return Ok(result);
        }              
    }
}
