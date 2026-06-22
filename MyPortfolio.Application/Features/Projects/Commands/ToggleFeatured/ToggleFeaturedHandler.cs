using MediatR;
using MyPortfolio.Application.Features.Projects.Commands.ToggleProject;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.Projects.Commands.ToggleFeatured
{
    public class ToggleFeaturedHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<ToggleFeaturedCommand, bool>
    {
        public async Task<bool> Handle(ToggleFeaturedCommand request, CancellationToken ct)
        {
            var project = await projectRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Project {request.Id} không tồn tại");

            project.ToggleFeatured();
            await projectRepository.UpdateAsync(project, ct);
            await unitOfWork.SaveChangesAsync(ct);
            return project.IsFeatured;
        }
    }
}
