using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.ArchiveMessage
{
    public record ArchiveMessageCommand(Guid Id) : IRequest;
}
