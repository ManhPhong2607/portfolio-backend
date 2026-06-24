using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.MarkMessageRead
{
    public class MarkMessageReadValidator : AbstractValidator<MarkMessageReadCommand>
    {
        public MarkMessageReadValidator()
            => RuleFor(x => x.Id).NotEmpty();
    }
}
