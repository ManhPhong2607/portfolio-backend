using MediatR;
using MyPortfolio.Application.Features.Projects.DTOs;
using MyPortfolio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.Queries.GetProjectList
{
    public record GetProjectListQuery(
        int Page = 1,
        int Limit = 12,
        Guid? TechnologyId = null
        ) : IRequest<PaginatedResult<ProjectSummaryDto>>;
}
