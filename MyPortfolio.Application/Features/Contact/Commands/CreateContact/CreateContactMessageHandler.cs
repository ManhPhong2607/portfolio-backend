using MediatR;
using Microsoft.Extensions.Logging;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Commands.CreateContact
{
    public class CreateContactMessageHandler(
     IContactMessageRepository messageRepository,
     IEmailNotificationService emailService,
     IUnitOfWork unitOfWork,
     ILogger<CreateContactMessageHandler> logger
 ) : IRequestHandler<CreateContactMessageCommand>
    {
        public async Task Handle(CreateContactMessageCommand request, CancellationToken ct)
        {
            // Honeypot: bot tự điền field ẩn này → âm thầm bỏ qua
            if (!string.IsNullOrWhiteSpace(request.HoneypotUrl))
                return;

            var message = ContactMessage.Create(
                request.Name, request.Email, request.Message, request.Subject);

            // Luồng chính — luôn phải thành công
            await messageRepository.AddAsync(message, ct);
            await unitOfWork.SaveChangesAsync(ct);

            // Luồng phụ — không được làm fail request, dù Resend hết hạn/lỗi
            try
            {
                await emailService.NotifyNewContactMessageAsync(message, ct);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex,
                    "Không gửi được email thông báo cho message {MessageId}, " +
                    "nhưng tin nhắn đã lưu thành công.", message.Id);
            }
        }
    }
}
