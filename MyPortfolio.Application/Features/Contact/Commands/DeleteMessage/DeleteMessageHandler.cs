using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.DeleteMessage
{
    public class DeleteMessageHandler(
      IContactMessageRepository repository,
      IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteMessageCommand>
    {
        public async Task Handle(DeleteMessageCommand request, CancellationToken ct)
        {
            var msg = await repository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Message {request.Id} không tồn tại.");

            await repository.DeleteAsync(msg, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
