using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.ArchiveMessage
{
    public class ArchiveMessageHandler(
        IContactMessageRepository repository,
        IUnitOfWork unitOfWork
    ) : IRequestHandler<ArchiveMessageCommand>
    {
        public async Task Handle(ArchiveMessageCommand request, CancellationToken ct)
        {
            var msg = await repository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Message {request.Id} không tồn tại.");

            msg.Archive();
            await repository.UpdateAsync(msg, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
