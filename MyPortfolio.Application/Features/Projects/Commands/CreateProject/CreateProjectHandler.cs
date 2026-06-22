using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectHandler(
        IProjectRepository projectRepository,
        ITechnologyRepository technologyRepository,
        IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateProjectCommand, Guid>
    {
        public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken ct)
        {
            var project = Project.Create(
                title: request.Title,
                shortDescription: request.ShortDescription,
                detailContent: request.DetailContent,
                demoUrl: request.DemoUrl,
                githubUrl: request.GithubUrl,
                startDate: request.StartDate,
                endDate: request.EndDate,
                thumbnailMediaId: request.ThumbnailMediaId
                );

            var slugExists = await projectRepository.SlugExistAsync(
                project.Slug.Value, ct: ct);
            if(slugExists)
                throw new DomainException($"Slug '{project.Slug.Value}' đã tồn tại.");

            //project.Update(
            //    title: request.Title,
            //    shortDescription: request.ShortDescription,
            //    detailContent: request.DetailContent,
            //    demoUrl: request.DemoUrl,
            //    githubUrl: request.GithubUrl,
            //    status: request.Status,
            //    startDate: request.StartDate,
            //    endDate: request.EndDate,
            //    thumbnailMediaId: request.ThumbnailMediaId
            //);

            if (request.TechnologyIds.Count > 0)
            {
                var technologies = await technologyRepository.GetAllAsync(ct);
                var selected = technologies
                    .Where(t => request.TechnologyIds.Contains(t.Id))
                    .ToList();  
                project.SetTechnologies(selected);
            }

            await projectRepository.AddAsync(project, ct);
            await unitOfWork.SaveChangesAsync(ct);
            return project.Id;
        }
    }
}
