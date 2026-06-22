using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyPortfolio.Application.Features.Auth.DTOs;

namespace MyPortfolio.Application.Features.Auth.Commands.Logout
{
    public record LogoutCommand
    (
        string RefreshToken
    ) : IRequest;
    
}
