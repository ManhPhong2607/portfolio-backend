using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.UnarchiveMessage
{
    public record UnarchiveMessageCommand(Guid Id) : IRequest;
}
