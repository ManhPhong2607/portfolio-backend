using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.CreateContact
{
    public class CreateContactMessageValidator : AbstractValidator<CreateContactMessageCommand>
    {
        public CreateContactMessageValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Vui lòng nhập tên.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Vui lòng nhập email.")
                .EmailAddress().WithMessage("Email không hợp lệ.");

            RuleFor(x => x.Subject)
                .MaximumLength(150)
                .When(x => x.Subject is not null);

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Vui lòng nhập nội dung.")
                .MinimumLength(10).WithMessage("Nội dung quá ngắn.")
                .MaximumLength(2000);
        }
    }
}
