using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.DeleteMessage
{
    public record DeleteMessageCommand(Guid Id) : IRequest;
}
