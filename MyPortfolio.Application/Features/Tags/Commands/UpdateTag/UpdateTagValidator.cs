using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Tags.Commands.UpdateTag
{
    public class UpdateTagValidator :AbstractValidator<UpdateTagCommand>
    {
        public UpdateTagValidator() 
        {
            RuleFor(t => t.Id).NotEmpty()
                .WithMessage("id không được rỗng");

            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Tên Tag không được rỗng")
                .MaximumLength(100).WithMessage("Tên tag tối đa 100 kí tự");
        }
    }
}
