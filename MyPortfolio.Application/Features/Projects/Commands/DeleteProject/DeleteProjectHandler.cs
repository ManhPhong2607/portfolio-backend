using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectHandler(
        IProjectRepository projectRepository,
        ITechnologyRepository technologyRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteProjectCommand>
    {
        public async Task Handle(DeleteProjectCommand request, CancellationToken ct)
        {
            var project = await projectRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Project {request.Id} không tồn tại");
            await projectRepository.DeleteAsync(project, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
