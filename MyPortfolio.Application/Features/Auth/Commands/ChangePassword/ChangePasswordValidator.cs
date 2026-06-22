using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator() 
        {
            RuleFor(x => x.OldPassword).NotEmpty()
                .WithMessage("Mật khẩu cũ không được rỗng");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Mật khẩu mới không được rỗng")
                .MinimumLength(6).WithMessage("Mật khẩu mới phải ít nhất 6 kí tự")
                .NotEqual(x => x.OldPassword).WithMessage("Mật khẩu mới không được trùng mật khẩu cũ");

            RuleFor(x => x.ConfirmNewPassword)
                .Equal(x => x.NewPassword).WithMessage("Xác nhận mật khẩu không khớp.");
       
        }
    }
}
