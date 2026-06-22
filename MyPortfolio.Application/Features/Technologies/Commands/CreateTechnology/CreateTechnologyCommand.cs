using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Technologies.Commands.CreateTechnology
{
    public record CreateTechnologyCommand(
    string Name,
    string? IconUrl
    ) : IRequest<Guid>;
}
