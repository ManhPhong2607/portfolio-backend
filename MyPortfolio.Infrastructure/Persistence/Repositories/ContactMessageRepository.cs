using Microsoft.EntityFrameworkCore;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Enums;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Infrastructure.Persistence.Repositories
{
    public class ContactMessageRepository
        (ApplicationDbContext context) : IContactMessageRepository
    {
        public async Task<PaginatedResult<ContactMessage>> GetAllAsync(int page, int limit, MessageStatus? status = null, CancellationToken ct = default)
        {
            var query = context.ContactMessages.AsQueryable();

            if (status.HasValue)
                query = query.Where(m => m.Status == status.Value);

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(m => m.SentAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(ct);

            return new PaginatedResult<ContactMessage>(items, total, page, limit);
        }

        public async Task<ContactMessage?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await context.ContactMessages.FindAsync([id], ct);
        }


        public async Task AddAsync(ContactMessage message, CancellationToken ct = default)
        {
             await context.ContactMessages.AddAsync(message, ct);
        }

        public async Task<int> CountUnreadAsync(CancellationToken ct = default)
        {
            return await context.ContactMessages.CountAsync(m => m.Status == MessageStatus.Unread, ct);
        }

        public Task DeleteAsync(ContactMessage message, CancellationToken ct = default)
        {
            context.ContactMessages.Remove(message);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(ContactMessage message, CancellationToken ct = default)
        {
            context.ContactMessages.Update(message);
            return Task.CompletedTask;
        }
    }
}
