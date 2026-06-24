using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.DTOs
{
    public record ContactMessageDto
    (
        Guid Id,
        string SenderName,
        string SenderEmail,
        string? Subject,
        string Body,
        string Status,
        DateTime  SentAt,
        DateTime? ReadAt
    );
}
