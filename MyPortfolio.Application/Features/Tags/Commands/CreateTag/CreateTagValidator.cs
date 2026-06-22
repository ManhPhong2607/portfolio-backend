using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagValidator : AbstractValidator<CreateTagCommand>
    {   
        public CreateTagValidator() 
        {
            RuleFor(t => t.Name).NotEmpty()
                .WithMessage(" Tên tag không được rỗng")
                .MaximumLength(100).WithMessage("Tên tag không quá 100 kí tự.");
        }
    }
}
