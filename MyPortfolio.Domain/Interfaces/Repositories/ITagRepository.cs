using MyPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Interfaces.Repositories
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllAsync(CancellationToken ct = default);
        Task<Tag?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Tag?> GetBySlugAsync(string slug, CancellationToken ct = default);
        //Task<bool> IsUsedAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Tag tag, CancellationToken ct = default);
        Task UpdateAsync(Tag tag, CancellationToken ct = default);
        Task DeleteAsync(Tag tag, CancellationToken ct = default);
        Task<Dictionary<Guid, int>> GetBlogCountsAsync(CancellationToken ct = default);
    }
}
