using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.ArchiveMessage
{
    public class ArchiveMessageValidator : AbstractValidator<ArchiveMessageCommand>
    {
        public ArchiveMessageValidator()
            => RuleFor(x => x.Id).NotEmpty();
    }
}
