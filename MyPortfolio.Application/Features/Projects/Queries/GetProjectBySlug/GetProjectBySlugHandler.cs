using MediatR;
using MyPortfolio.Application.Features.Projects.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Enums;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Application.Features.Projects.DTOs;

using MyPortfolio.Application.Features.Technologies.DTOs;
namespace MyPortfolio.Application.Features.Projects.Queries.GetProjectBySlug
{
    public class GetProjectBySlugHandler(IProjectRepository projectRepository)
        : IRequestHandler<GetProjectBySlugQuery, ProjectDetailDto>
    {
        public async Task<ProjectDetailDto> Handle(GetProjectBySlugQuery request, CancellationToken ct)
        {
            var project = await projectRepository.GetBySlugAsync(request.Slug, ct)
                ?? throw new NotFoundException($"Project '{request.Slug}' không tồn tại");

            if (project.Status == ProjectStatus.Archived)
                throw new NotFoundException($"Project '{request.Slug}' không tồn tại");

            return new ProjectDetailDto(
                Id: project.Id,
                Title: project.Title,
                Slug: project.Slug.Value,
                ShortDescription: project.ShortDescription,
                DetailContent: project.DetailContent,
                ThumbnailUrl: project.ThumbnailMedia?.SecureUrl,
                ThumbnailMediaId: project.ThumbnailMediaId,
                DemoUrl: project.DemoUrl,
                GithubUrl: project.GithubUrl,
                Status: project.Status,
                DisplayOrder: project.DisplayOrder,
                IsFeatured: project.IsFeatured,
                StartDate: project.StartDate,
                EndDate: project.EndDate,
                CreatedAt: project.CreatedAt,
                UpdatedAt: project.UpdateAt,
                Technologies: project.Technologies
                    .Select(t => new ProjectTechnologyDto(t.Id, t.Name, t.IconUrl))
                    .ToList()
            );
            //return MapToDetail(project);
        }

     //   public static ProjectDetailDto MapToDetail(Domain.Entities.Project p) => new(
     //    Id: p.Id,
     //    Title: p.Title,
     //    Slug: p.Slug.Value,
     //    ShortDescription: p.ShortDescription,
     //    DetailContent: p.DetailContent,
     //    ThumbnailUrl: p.ThumbnailMedia?.SecureUrl,
     //    DemoUrl: p.DemoUrl,
     //    GithubUrl: p.GithubUrl,
     //    Status: p.Status,
     //    DisplayOrder: p.DisplayOrder,
     //    IsFeatured: p.IsFeatured,
     //    StartDate: p.StartDate,
     //    EndDate: p.EndDate,
     //    CreatedAt: p.CreatedAt,
     //    UpdatedAt: p.UpdateAt,
     //    Technologies: p.Technologies
     //        .Select(t => new TechnologyDto(t.Id, t.Name, t.IconUrl))
     //        .ToList()
     //);

    }
}
