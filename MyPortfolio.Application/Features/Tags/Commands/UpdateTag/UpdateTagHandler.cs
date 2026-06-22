using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.Tags.Commands.UpdateTag
{
    public class UpdateTagHandler(ITagRepository repository, IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateTagCommand>
    {
        public async Task Handle(UpdateTagCommand request, CancellationToken ct)
        {
            var tag = await repository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Tag {request.Id} không tồn tại.");

            var exist = await repository.GetAllAsync(ct);
            if (exist.Any(t => t.Id != request.Id && t.Name.Equals(request.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                throw new DomainException($"Tag '{request.Name}' đã tồn tại.");

            tag.Update(request.Name);
            await repository.UpdateAsync(tag, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
