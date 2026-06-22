using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.Commands.ChangeProjectStatus
{
    public class ChangeProjectStatusHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<ChangeProjectStatusCommand>
    {
        public async Task Handle(ChangeProjectStatusCommand request, CancellationToken ct)
        {
            var project = await projectRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Project {request.Id} không tồn tại");

            project.ChangeStatus(request.Status);
            await projectRepository.UpdateAsync(project, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
