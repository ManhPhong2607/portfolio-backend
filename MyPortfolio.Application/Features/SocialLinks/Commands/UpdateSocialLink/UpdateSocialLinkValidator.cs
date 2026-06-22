using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.SocialLinks.Commands.UpdateSocialLink
{
    public class UpdateSocialLinkValidator :AbstractValidator<UpdateSocialLinkCommand>
    {
        public UpdateSocialLinkValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được rỗng.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url không được rỗng.")
                .MaximumLength(500).WithMessage("Url tối đa 500 kí tự.");

            RuleFor(x => x.Label)
                .MaximumLength(100).WithMessage("Label tối đa 100 kí tự.")
                .When(x => x.Label != null);

        }
    }
}
