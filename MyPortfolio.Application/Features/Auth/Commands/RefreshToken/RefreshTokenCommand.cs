using MyPortfolio.Application.Features.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MyPortfolio.Application.Features.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand
    (string RefreshToken): IRequest<LoginResult>;
    
}
