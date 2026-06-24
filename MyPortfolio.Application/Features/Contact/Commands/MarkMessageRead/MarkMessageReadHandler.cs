using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.MarkMessageRead
{
    public class MarkMessageReadHandler(
        IContactMessageRepository repository,
        IUnitOfWork unitOfWork
    ) : IRequestHandler<MarkMessageReadCommand>
    {
        public async Task Handle(MarkMessageReadCommand request, CancellationToken ct)
        {
            var msg = await repository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Message {request.Id} không tồn tại.");

            msg.MarkAsRead();
            await repository.UpdateAsync(msg, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
