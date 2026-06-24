using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Interfaces.Repositories
{
    public interface IContactMessageRepository
    {
        Task AddAsync(ContactMessage message, CancellationToken ct = default);
        Task<ContactMessage?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<PaginatedResult<ContactMessage>> GetAllAsync(int page, int limit
            , MessageStatus? status = null, CancellationToken ct = default);
        Task UpdateAsync(ContactMessage message, CancellationToken ct = default);
        Task<int> CountUnreadAsync(CancellationToken ct = default);
        Task DeleteAsync(ContactMessage message, CancellationToken ct = default);
    }
}
