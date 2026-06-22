using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyValidator : AbstractValidator<UpdateTechnologyCommand>
    {
        public UpdateTechnologyValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được rỗng.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên technology không được rỗng.")
                .MaximumLength(100).WithMessage("Tên tối đa 100 ký tự.");
        }
    }
}
