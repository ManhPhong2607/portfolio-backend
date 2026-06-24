using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.Contact.Commands.ArchiveMessage;
using MyPortfolio.Application.Features.Contact.Commands.DeleteMessage;
using MyPortfolio.Application.Features.Contact.Commands.MarkMessageRead;
using MyPortfolio.Application.Features.Contact.Commands.UnarchiveMessage;
using MyPortfolio.Application.Features.Contact.Queries.GetMessages;
using MyPortfolio.Domain.Enums;

namespace MyPortfolio.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/messages")]
    [Authorize]
    public class AdminMessagesController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10,
            [FromQuery] MessageStatus? status = null,
            CancellationToken ct = default)
        {
            var messages = await mediator.Send(new GetMessagesQuery(page,limit, status), ct);
            return Ok(messages);
        }

        [HttpPatch("{id:guid}/read")]
        public async Task<IActionResult> MarkRead(Guid id, CancellationToken ct)
        {
            await mediator.Send(new MarkMessageReadCommand(id), ct);
            return NoContent();
        }

        [HttpPatch("{id:guid}/archive")]
        public async Task<IActionResult> Archive(Guid id, CancellationToken ct)
        {
            await mediator.Send(new ArchiveMessageCommand(id), ct);
            return NoContent();
        }

        [HttpPatch("{id:guid}/unarchive")]
        public async Task<IActionResult> Unarchive(Guid id, CancellationToken ct)
        {
            await mediator.Send(new UnarchiveMessageCommand(id), ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await mediator.Send(new DeleteMessageCommand(id), ct);
            return NoContent();
        }
    }
}
