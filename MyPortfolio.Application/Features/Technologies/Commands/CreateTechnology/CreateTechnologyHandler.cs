using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyHandler(
     ITechnologyRepository repository,
     IUnitOfWork unitOfWork
     ) : IRequestHandler<CreateTechnologyCommand, Guid>
    {
        public async Task<Guid> Handle(CreateTechnologyCommand request, CancellationToken ct)
        {
            // Kiểm tra tên trùng
            var existing = await repository.GetAllAsync(ct);
            if (existing.Any(t => t.Name.Equals(request.Name.Trim(),
                    StringComparison.OrdinalIgnoreCase)))
                throw new DomainException($"Technology '{request.Name}' đã tồn tại.");

            var technology = Technology.Create(request.Name, request.IconUrl);
            await repository.AddAsync(technology, ct);
            await unitOfWork.SaveChangesAsync(ct);
            return technology.Id;
        }
    }
}
