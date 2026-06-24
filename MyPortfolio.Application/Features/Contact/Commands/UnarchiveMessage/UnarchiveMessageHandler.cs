using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.UnarchiveMessage
{
    public class UnarchiveMessageHandler(
       IContactMessageRepository repository,
       IUnitOfWork unitOfWork
    ) : IRequestHandler<UnarchiveMessageCommand>
    {
        public async Task Handle(UnarchiveMessageCommand request, CancellationToken ct)
        {
            var msg = await repository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Message {request.Id} không tồn tại.");

            msg.Unarchive();
            await repository.UpdateAsync(msg, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
