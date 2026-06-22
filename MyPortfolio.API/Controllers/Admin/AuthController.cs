using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.Auth.Commands.Login;
using MyPortfolio.Application.Features.Auth.Commands.RefreshToken;
using MyPortfolio.Application.Features.Auth.Commands.ChangePassword;
using MyPortfolio.Application.Features.Auth.Commands.Logout;
using System.Net.WebSockets;
namespace MyPortfolio.API.Controllers.Admin
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]
    public class AuthController(ISender mediator) : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken ct)
        {
            var result = await mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command, CancellationToken ct)
        {
            var result = await mediator.Send(command, ct);
            return Ok(result);
        }
        //[Authorize]
        //[HttpGet("me")]
        //public IActionResult Me()
        //{
        //    return Ok(User.Identity?.IsAuthenticated);
        //}

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command, CancellationToken ct) 
        {
            await mediator.Send(command,ct);
            return NoContent();
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand command, CancellationToken ct)
        {
            await mediator.Send(command,ct);
            return NoContent();
        } 
    }
}
