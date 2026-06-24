using MyPortfolio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Enums;
using MyPortfolio.Domain.ValueObjects;

namespace MyPortfolio.Domain.Entities
{
    public class ContactMessage : BaseEntity
    {
        public string SenderName { get; private set; } = default!;
        public Email SenderEmail { get; private set; } = default!;
        public string? Subject { get; private set; }
        public string Body { get; private set; } = default!;
        public MessageStatus Status { get; private set; }
        public MessageStatus? PreviousStatus { get; private set; }
        public DateTime SentAt { get; private set; }
        public DateTime? ReadAt { get; private set; }

        private ContactMessage() { }

        public static ContactMessage Create(string senderName, string senderEmail,
            string body, string? subject = null)
        {
            if (string.IsNullOrWhiteSpace(senderName))
                throw new DomainException("Tên người gửi không được rỗng.");

            if (string.IsNullOrWhiteSpace(body))
                throw new DomainException("Nội dung không được rỗng.");

            return new ContactMessage
            {
                SenderName = senderName.Trim(),
                SenderEmail = Email.Create(senderEmail),
                Subject = subject?.Trim(),
                Body = body.Trim(),
                Status = MessageStatus.Unread,
                SentAt = DateTime.UtcNow
            };
        }

        public void MarkAsRead()
        {
            if (Status != MessageStatus.Unread) return;
            if (Status == MessageStatus.Archived)
                throw new DomainException(
                    "Không thể đánh dấu đã đọc khi tin nhắn đang lưu trữ. Hãy Unarchive trước.");
            Status = MessageStatus.Read;
            ReadAt = DateTime.UtcNow;
            SetUpdated();
        }

        public void Archive()
        {
            if (Status == MessageStatus.Archived) return; 
            PreviousStatus = Status;
            Status = MessageStatus.Archived;
            SetUpdated();
        }

        public void Unarchive()
        {
            //if (Status != MessageStatus.Archived)
            //    return;

            //if (PreviousStatus is null)
            //    throw new DomainException("Không có trạng thái trước đó.");

            //Status = PreviousStatus.Value;
            //PreviousStatus = null;

            //SetUpdated();
            if (Status != MessageStatus.Archived)
                throw new DomainException("Tin nhắn không ở trạng thái lưu trữ.");

            Status = PreviousStatus ?? MessageStatus.Read; // khôi phục đúng trạng thái cũ
            PreviousStatus = null;
            SetUpdated();
        }
    }

}

