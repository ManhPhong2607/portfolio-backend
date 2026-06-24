using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.UnarchiveMessage
{
    public class UnarchiveMessageValidator : AbstractValidator<UnarchiveMessageCommand>
    {
        public UnarchiveMessageValidator()
            => RuleFor(x => x.Id).NotEmpty();
    }
}
