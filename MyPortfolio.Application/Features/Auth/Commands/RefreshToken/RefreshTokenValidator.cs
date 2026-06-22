using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace MyPortfolio.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand> 
    {
        public RefreshTokenValidator() 
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token không được rỗng");
        }
    }
}
