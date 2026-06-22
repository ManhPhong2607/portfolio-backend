using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.Commands.ReorderProject
{
    public class ReorderProjectsHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<ReorderProjectsCommand>
    {
        public async Task Handle(ReorderProjectsCommand request, CancellationToken ct)
        {
            var orders = request.OrderedIds
                .Select((id, index) => (Id: id, Order: index))
                .ToList();
            await projectRepository.ReorderAsync(orders, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
