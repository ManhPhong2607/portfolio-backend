using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MyPortfolio.Application.Features.Contact.Commands.CreateContact;

namespace MyPortfolio.API.Controllers.Public
{
    [ApiController]
    [Route("api/contact")]
    [AllowAnonymous]
    [EnableRateLimiting("contact")]
    public class ContactController(ISender mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Send([FromBody] CreateContactMessageCommand command, CancellationToken ct)
        {
            await mediator.Send(command, ct);
            return Ok(new { message = "Tin nhắn đã được gửi. Cảm ơn bạn đã liên hệ!"});
        }    
    }
}
