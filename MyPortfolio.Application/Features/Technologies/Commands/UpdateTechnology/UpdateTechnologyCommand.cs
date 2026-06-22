using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Technologies.Commands.UpdateTechnology
{
    public record UpdateTechnologyCommand(
    Guid Id,
    string Name,
    string? IconUrl
    ) : IRequest;
}
