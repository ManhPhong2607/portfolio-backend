using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.Commands.ToggleProject
{
    public record ToggleFeaturedCommand(Guid Id) : IRequest<bool>;
}
