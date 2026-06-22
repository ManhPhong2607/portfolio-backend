using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.Dashboard.Queries;

namespace MyPortfolio.API.Controllers.Admin
{
    [ApiController]
    [Route("admin/dashboard")]
    [Authorize]
    public class AdminDashboardController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await mediator.Send(new GetDashboardQuery(), ct);
            return Ok(result);         
        }
       
    }
}
