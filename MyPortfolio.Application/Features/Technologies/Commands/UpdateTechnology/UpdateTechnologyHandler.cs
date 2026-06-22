using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyHandler(
    ITechnologyRepository repository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateTechnologyCommand>
    {
        public async Task Handle(UpdateTechnologyCommand request, CancellationToken ct)
        {
            var technology = await repository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Technology {request.Id} không tồn tại.");

            // Kiểm tra tên trùng với technology khác
            var existing = await repository.GetAllAsync(ct);
            if (existing.Any(t => t.Id != request.Id &&
                    t.Name.Equals(request.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                throw new DomainException($"Technology '{request.Name}' đã tồn tại.");

            technology.Update(request.Name, request.IconUrl);
            await repository.UpdateAsync(technology, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
