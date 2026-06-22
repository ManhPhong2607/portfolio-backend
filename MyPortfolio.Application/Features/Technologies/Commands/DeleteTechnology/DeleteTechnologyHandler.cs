using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Technologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyHandler(
    ITechnologyRepository repository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteTechnologyCommand, DeleteTechnologyResult>
    {
        public async Task<DeleteTechnologyResult> Handle(
            DeleteTechnologyCommand request, CancellationToken ct)
        {
            var technology = await repository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Technology {request.Id} không tồn tại.");

            var isUsed = await repository.IsUsedAsync(request.Id, ct);

            if (isUsed && !request.Force)
                return new DeleteTechnologyResult(
                    Deleted: false,
                    WasInUse: true,
                    Message: "Technology đang được dùng trong project. Dùng Force=true để xoá."
                );

            await repository.DeleteAsync(technology, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return new DeleteTechnologyResult(
                Deleted: true,
                WasInUse: isUsed,
                Message: isUsed
                    ? "Đã xoá và gỡ khỏi các projects."
                    : "Đã xoá thành công."
            );
        }
    }
}
