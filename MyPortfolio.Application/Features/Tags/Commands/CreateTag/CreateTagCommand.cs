using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Tags.Commands.CreateTag
{
    public record CreateTagCommand(string Name) : IRequest<Guid>;
    
}
