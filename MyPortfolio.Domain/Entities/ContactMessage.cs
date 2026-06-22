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
        public string SenderName { get; private set; }
        public Email SenderEmail { get; private set; }
        public string? Subject { get; private set; }
        public string Body { get; private set; }
        public MessageStatus Status { get; private set; }
        public DateTime SentAt { get; private set; }
        public DateTime? ReadAt { get; private set; }

        private ContactMessage() { }

    }
}
