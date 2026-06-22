using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Technologies.Commands.DeleteTechnology
{
    public record DeleteTechnologyCommand(
    Guid Id,
    bool Force = false
    ) : IRequest<DeleteTechnologyResult>;

    public record DeleteTechnologyResult(bool Deleted, bool WasInUse, string? Message);
}
