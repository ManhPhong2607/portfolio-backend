using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator() 
        {
            RuleFor(x=> x.Id).NotEmpty()
                .WithMessage("Id không được rỗng");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Tiêu đề không được rỗng")
                .MaximumLength(200).WithMessage("Tiêu đề tối đa 200 kí tự.");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("EndDate phải sau StartDate.")
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue);
        }
    }
}
