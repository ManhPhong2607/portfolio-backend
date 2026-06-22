using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Experiences.Commands.UpdateExperience
{
    public class UpdateExperienceValidator : AbstractValidator<UpdateExperienceCommand> 
    {
        public UpdateExperienceValidator() 
        {
            RuleFor(x=> x.Id).NotEmpty().WithMessage("Id không được rỗng.");

            RuleFor(x => x.CompanyName).NotEmpty()
                .WithMessage("tên công ty không được rỗng")
                .MaximumLength(200).WithMessage("tên công ty không quá 200 kí tự");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("vị trí không được rỗng")
                .MaximumLength(200).WithMessage("vị trí tối đa 200 kí tự");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Ngày bắt đầu không được rỗng")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Ngày bắt đầu không được trong tương lai");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("Ngày kết thúc phải sau ngày bắt đầu ")
                .When(x => !x.IsCurrent && x.EndDate.HasValue);

            RuleFor(x => x.EndDate)
                .NotNull().WithMessage("Ngày kết thúc bắt buộc nếu không phải công việc hiện tại")
                .When(x => !x.IsCurrent);
        }
    }
}
