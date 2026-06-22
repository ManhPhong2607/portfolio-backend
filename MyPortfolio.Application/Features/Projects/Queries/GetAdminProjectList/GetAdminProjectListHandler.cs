using MediatR;
using MyPortfolio.Application.Features.Projects.DTOs;
using MyPortfolio.Application.Features.Technologies.DTOs;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.Queries.GetAdminProjectList
{
    public class GetAdminProjectListHandler(IProjectRepository projectRepository)
        : IRequestHandler<GetAdminProjectListQuery, PaginatedResult<ProjectSummaryDto>>
    {
        public async Task<PaginatedResult<ProjectSummaryDto>> Handle(GetAdminProjectListQuery request, CancellationToken ct)
        {
            var result = await projectRepository.GetAllAsync(
                request.Page, request.Limit, request.Status, ct);

            var dtos = result.Items.Select(p => new ProjectSummaryDto(
                Id: p.Id,
                Title: p.Title,
                Slug: p.Slug.Value,
                ShortDescription: p.ShortDescription,
                ThumbnailUrl: p.ThumbnailMedia?.SecureUrl,
                DemoUrl: p.DemoUrl,
                GithubUrl: p.GithubUrl,
                Status: p.Status,
                DisplayOrder: p.DisplayOrder,
                IsFeatured: p.IsFeatured,
                StartDate: p.StartDate,
                EndDate: p.EndDate,
                Technologies: p.Technologies
                    .Select(t => new ProjectTechnologyDto(t.Id, t.Name, t.IconUrl))
                    .ToList()
            )).ToList();

            return new PaginatedResult<ProjectSummaryDto>(
                dtos, result.TotalCount, result.Page, result.Limit);
        }
    }
}
