using MediatR;
using MyPortfolio.Application.Features.Projects.DTOs;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.Queries.GetAdminProjectList
{
    public record GetAdminProjectListQuery(
       int Page = 1,
       int Limit = 12,
       ProjectStatus? Status = null
    ) : IRequest<PaginatedResult<ProjectSummaryDto>>;
}
