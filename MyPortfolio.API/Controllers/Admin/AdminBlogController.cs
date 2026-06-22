using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.BlogPosts.Commands.ChangeBlogStatus;
using MyPortfolio.Application.Features.BlogPosts.Commands.CreateBlog;
using MyPortfolio.Application.Features.BlogPosts.Commands.DeleteBlog;
using MyPortfolio.Application.Features.BlogPosts.Commands.UpdateBlog;
using MyPortfolio.Application.Features.BlogPosts.Queries.GetAdminBlogById;
using MyPortfolio.Application.Features.BlogPosts.Queries.GetAdminBlogList;
using MyPortfolio.Domain.Enums;

namespace MyPortfolio.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/blogs")]
    [Authorize]
    public class AdminBlogController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(
       [FromQuery] int page = 1,
       [FromQuery] int limit = 10,
       [FromQuery] PostStatus? status = null,
       CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAdminBlogListQuery(page, limit, status), ct);
            return Ok(result);
        }
      
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetAdminBlogByIdQuery(id), ct);
            return Ok(result);              
        }         

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateBlogCommand command, CancellationToken ct)
        {
            var id = await mediator.Send(command, ct);
            return Created(string.Empty, new { id });
           // return CreatedAtAction(nameof(GetById), new { id }, new { id });          
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateBlogCommand command, CancellationToken ct)
        {
            await mediator.Send(command with { Id = id }, ct);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(
            Guid id,
            [FromBody] ChangeBlogStatusCommand request,
            CancellationToken ct)
        {
            await mediator.Send( new ChangeBlogStatusCommand(id,request.Status),ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await mediator.Send(new DeleteBlogPostCommand(id), ct);
            return NoContent();
        }
    }
}
