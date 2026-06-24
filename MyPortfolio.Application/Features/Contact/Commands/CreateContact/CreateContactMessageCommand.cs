using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.CreateContact
{
    public record CreateContactMessageCommand(
       string Name,
       string Email,
       string Message,
       string? Subject = null,
       string? HoneypotUrl = null
     ) : IRequest;
}
