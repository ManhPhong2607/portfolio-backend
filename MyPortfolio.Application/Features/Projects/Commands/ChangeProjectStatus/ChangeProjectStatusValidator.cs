using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Enums;
namespace MyPortfolio.Application.Features.Projects.Commands.ChangeProjectStatus
{
    public class ChangeProjectStatusValidator : AbstractValidator<ChangeProjectStatusCommand>
    {
        public ChangeProjectStatusValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được rỗng.");

            //RuleFor(x => x.Status)
            //    .IsInEnum().WithMessage("Status không hợp lệ.")
            //    .Must(s => s != ProjectStatus.Draft)
            //    .WithMessage("Không thể chuyển về Draft sau khi đã publish. Dùng Archive để ẩn.");
        }
    }
}
