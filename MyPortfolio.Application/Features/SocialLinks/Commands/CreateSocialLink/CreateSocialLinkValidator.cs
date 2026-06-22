using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.SocialLinks.Commands.CreateSocialLink
{
    public class CreateSocialLinkValidator : AbstractValidator<CreateSocialLinkCommand>
    {
        private static readonly String[] AllowedPlatforms =
              ["GitHub", "Twitter", "Facebook", "Instagram", "YouTube", "Email", "TikTok"];

        public CreateSocialLinkValidator()
        {
            RuleFor(x => x.Platform)
                .NotEmpty().WithMessage("Platform không được rỗng.")
                .Must(p => AllowedPlatforms.Contains(p, StringComparer.OrdinalIgnoreCase))
                //.Must(p=> AllowedPlatforms.Contains(p.ToLowerInvariant()))
                .WithMessage($"Platform phải là một trong:{string.Join(",", AllowedPlatforms)}");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL không được rỗng.")
                .MaximumLength(500).WithMessage("URL tối đa 500 kí tự.");
         
            RuleFor(x => x.Label)
                .MaximumLength(100).WithMessage("Label tối đa 100 kí tự.")
                .When(x => x.Label != null);
        }
    }
}
