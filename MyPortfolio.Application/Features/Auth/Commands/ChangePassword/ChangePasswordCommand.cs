using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Auth.Commands.ChangePassword
{
    public record ChangePasswordCommand(
        string OldPassword,
        string NewPassword,
        string ConfirmNewPassword) : IRequest;
      
}
