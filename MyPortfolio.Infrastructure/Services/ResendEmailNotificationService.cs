using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces.Services;
using MyPortfolio.Infrastructure.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Infrastructure.Services
{
    public class ResendEmailNotificationService(
        IHttpClientFactory httpClientFactory,
        IOptions<ResendOptions> options,
        ILogger<ResendEmailNotificationService> logger
        ) : IEmailNotificationService
    {
        private readonly ResendOptions _options = options.Value;

        public async Task NotifyNewContactMessageAsync(ContactMessage message, CancellationToken ct = default)
        {
            try
            {
                var client = httpClientFactory.CreateClient("Resend");

                var bodyText = $"""
                Tin nhắn mới từ Portfolio

                Tên: {message.SenderName}
                Email: {message.SenderEmail.Value}
                {(string.IsNullOrEmpty(message.Subject) ? "" : $"Chủ đề: {message.Subject}\n")}
                Nội dung:
                {message.Body}

                ---
                Gửi lúc: {message.SentAt:dd/MM/yyyy HH:mm} (UTC)
                """;

                var payload = new
                {
                    from = $"Portfolio Contact <{_options.FromEmail}>",
                    to = new[] { _options.ToEmail },
                    subject = string.IsNullOrEmpty(message.Subject)
                        ? $"Tin nhắn mới từ {message.SenderName}"
                        : $"[{message.Subject}] từ {message.SenderName}",
                    text = bodyText,
                    reply_to = message.SenderEmail.Value,
                };

                var response = await client.PostAsJsonAsync("emails", payload, ct);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync(ct);
                    logger.LogWarning(
                        "Resend trả lỗi {StatusCode}: {Error}", response.StatusCode, error);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Lỗi khi gửi email thông báo qua Resend.");
            }
        }
    }
}
