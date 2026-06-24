using MyPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Interfaces.Services
{
    public interface IEmailNotificationService
    {
        Task NotifyNewContactMessageAsync(ContactMessage message, CancellationToken ct = default);
    }
}
