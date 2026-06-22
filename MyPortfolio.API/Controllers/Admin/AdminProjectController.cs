using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.Projects.Commands.ChangeProjectStatus;
using MyPortfolio.Application.Features.Projects.Commands.CreateProject;
using MyPortfolio.Application.Features.Projects.Commands.DeleteProject;
using MyPortfolio.Application.Features.Projects.Commands.ReorderProject;
using MyPortfolio.Application.Features.Projects.Commands.ToggleProject;
using MyPortfolio.Application.Features.Projects.Commands.UpdateProject;
using MyPortfolio.Application.Features.Projects.Queries.GetAdminProjectById;
using MyPortfolio.Application.Features.Projects.Queries.GetAdminProjectList;
using MyPortfolio.Domain.Enums;

namespace MyPortfolio.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/projects")]
    [Authorize]
    public class AdminProjectController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int limit = 12,
            [FromQuery] ProjectStatus? status = null,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAdminProjectListQuery(page, limit, status), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetAdminProjectByIdQuery(id), ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectCommand command, CancellationToken ct)
        {
            var id = await mediator.Send(command, ct);
            return Created(string.Empty, new { id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateProjectCommand command, CancellationToken ct)
        {
            await mediator.Send(command with { Id = id }, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await mediator.Send(new DeleteProjectCommand(id), ct);
            return NoContent();
        }

        [HttpPatch("reorder")]
        public async Task<IActionResult> Reorder(
             [FromBody] ReorderProjectsCommand command, CancellationToken ct)
        {
            await mediator.Send(command, ct);
            return NoContent();
        }

        [HttpPatch("{id:guid}/featured")]
        public async Task<IActionResult> ToggleFeatured(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new ToggleFeaturedCommand(id), ct);
            return Ok(new { result });
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> ChangeStatus(
            Guid id, [FromBody] ChangeProjectStatusCommand request, CancellationToken ct)
        {
            await mediator.Send(request with { Id = id }, ct);
            return NoContent();
        }

        
    }
}
