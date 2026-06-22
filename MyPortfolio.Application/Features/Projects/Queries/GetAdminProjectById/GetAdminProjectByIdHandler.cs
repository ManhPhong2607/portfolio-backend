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

namespace MyPortfolio.Application.Features.Projects.Queries.GetAdminProjectById
{
    public class GetAdminProjectByIdHandler(IProjectRepository projectRepository)
        : IRequestHandler<GetAdminProjectByIdQuery, ProjectDetailDto>
    {
        public async Task<ProjectDetailDto> Handle(GetAdminProjectByIdQuery request, CancellationToken ct)
        {
            var project = await projectRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Project {request.Id} không tồn tại");

            return new ProjectDetailDto(
               Id: project.Id,
               Title: project.Title,
               Slug: project.Slug.Value,
               ShortDescription: project.ShortDescription,
               DetailContent: project.DetailContent,
               ThumbnailUrl: project.ThumbnailMedia?.SecureUrl,
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
        }
    }
}
