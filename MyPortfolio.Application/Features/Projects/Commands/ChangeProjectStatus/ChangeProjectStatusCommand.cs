using MediatR;
using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.Commands.ChangeProjectStatus
{
    public record ChangeProjectStatusCommand(
        Guid Id,
        ProjectStatus Status
    ): IRequest;

}
