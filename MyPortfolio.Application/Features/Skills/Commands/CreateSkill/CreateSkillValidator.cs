using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Skills.Commands.CreateSkill
{
    public class CreateSkillValidator : AbstractValidator<CreateSkillCommand>
    {
        public CreateSkillValidator() 
        {
            RuleFor(x => x.Name).NotEmpty()
               .WithMessage("Tên kỹ năng không được rỗng")
               .MaximumLength(100).WithMessage("Tên tối đa 100 kí tự");

            RuleFor(x => x.ProficiencyLevel)
                    .InclusiveBetween(1, 5).WithMessage("Mức độ phải từ 1 đến 5");
        }
       
    }
}
