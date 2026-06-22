using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.About.Commands.UpdateAboutProfile
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileValidator() 
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Tên không được rỗng")
                .MaximumLength(50).WithMessage("Tên tối đa 50 kí tự");

            RuleFor(x => x.Tagline)
                .MaximumLength(200).WithMessage("Tagline tối đa 200 kí tự")
                .When(x => x.Tagline != null);

             RuleFor(x => x.ContactEmail)
                .EmailAddress().WithMessage("Email không hợp lệ")
                .When(x => !string.IsNullOrWhiteSpace(x.ContactEmail));
        }
    }
    
}
