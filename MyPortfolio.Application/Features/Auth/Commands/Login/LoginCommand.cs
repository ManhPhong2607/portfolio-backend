using MediatR;
using MyPortfolio.Application.Features.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Auth.Commands.Login
{
    public record LoginCommand
    (
        string Email,
        string Password
    ) : IRequest<LoginResult>;

    //public class LoginCommand : IRequest<LoginResult>
    //{
    //    public string Email { get; init; }
    //    public string Password { get; init; }

    //    public LoginCommand(string email, string password)
    //    {
    //        Email = email;
    //        Password = password;
    //    }
    //}
}
