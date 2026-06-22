using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.Tags.Commands.DeleteTag
{
    public class DeleteTagHandler(ITagRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteTagCommand>
    {
        public async Task Handle(DeleteTagCommand request, CancellationToken ct)
        {
            var tag = await repository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Tag {request.Id} không tồn tại");

            //var isUsed = await repository.IsUsedAsync(request.Id, ct);
            //if (isUsed && !request.Force)
            //    return new DeleteTagResult(
            //        Deleted: false,
            //        WasInUse: true,
            //        Message: "Tag đang được dùng trong bài viết. Dùng Force=true để xoá bắt buộc."
            //    );

            await repository.DeleteAsync(tag, ct);
            await unitOfWork.SaveChangesAsync(ct);

            //return new DeleteTagResult(
            //Deleted: true,
            //WasInUse: isUsed,
            //Message: isUsed ? "Đã xoá tag và gỡ khỏi các bài viết." : "Đã xoá tag."
            //);
        }
    }
}
