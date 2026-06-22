using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Auth.Commands.Logout
{
    public class LogoutValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutValidator() 
        {
            RuleFor(x => x.RefreshToken).NotEmpty()
                .WithMessage("Refresh token không được rỗng");
        }
    }
}
