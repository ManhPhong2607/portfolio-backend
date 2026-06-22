using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyValidator : AbstractValidator<CreateTechnologyCommand> 
    {
        public CreateTechnologyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên technology không được rỗng.")
                .MaximumLength(100).WithMessage("Tên tối đa 100 ký tự.");
        }
    }
}
