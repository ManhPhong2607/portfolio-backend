using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Application.Features.Projects.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Application.Features.Technologies.DTOs;

namespace MyPortfolio.Application.Features.Projects.Queries.GetProjectList
{
    public class GetProjectListHandler(
        IProjectRepository projectRepository
        ) : IRequestHandler<GetProjectListQuery, PaginatedResult<ProjectSummaryDto>>
    {
        public async Task<PaginatedResult<ProjectSummaryDto>> Handle(GetProjectListQuery request, CancellationToken ct)
        {
            var result = await projectRepository.GetPublishedAsync(
                request.Page, request.Limit, request.TechnologyId, ct);

            var dtos = result.Items.Select(p => new ProjectSummaryDto(
                Id: p.Id,
                Title: p.Title,
                Slug: p.Slug,
                ShortDescription: p.ShortDescription,
                ThumbnailUrl: p.ThumbnailMedia?.SecureUrl,
                DemoUrl: p.DemoUrl,
                GithubUrl: p.GithubUrl,
                Status: p.Status,
                DisplayOrder: p.DisplayOrder,
                IsFeatured: p.IsFeatured,
                StartDate: p.StartDate,
                EndDate: p.EndDate,
                Technologies: p.Technologies.Select(t => new ProjectTechnologyDto(t.Id, t.Name, t.IconUrl))
                .ToList()
                )).ToList();
            return new PaginatedResult<ProjectSummaryDto>(
                dtos, result.TotalCount, result.Page, result.Limit);
        }
    }

}
