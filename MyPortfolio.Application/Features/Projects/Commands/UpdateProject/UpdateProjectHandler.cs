using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.ValueObjects;
namespace MyPortfolio.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectHandler(
        IProjectRepository projectRepository,
        ITechnologyRepository technologyRepository,
        IUnitOfWork unitOfWork
     ) : IRequestHandler<UpdateProjectCommand>
    {
        public async Task Handle(UpdateProjectCommand request, CancellationToken ct)
        {
            var project = await projectRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Project {request.Id} không tồn tại");

            project.Update(
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
                project.Slug.Value, excludedId: request.Id, ct: ct);
            if (slugExists)
                throw new DomainException($"Slug '{project.Slug.Value}' đã tồn tại.");

            var technologies = await technologyRepository.GetAllAsync(ct);
            var selected = technologies
                .Where(t => request.TechnologyIds.Contains(t.Id))
                .ToList();
            project.SetTechnologies(selected);

            await projectRepository.UpdateAsync(project, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
