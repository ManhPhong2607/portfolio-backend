using MyPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Interfaces.Repositories
{
    public interface ITechnologyRepository
    {
        Task<List<Technology>> GetAllAsync(CancellationToken ct = default);
        Task<Technology?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> IsUsedAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Technology technology, CancellationToken ct = default);
        Task UpdateAsync(Technology technology, CancellationToken ct = default);
        Task DeleteAsync(Technology technology, CancellationToken ct = default);
        Task<Dictionary<Guid, int>> GetProjectCountsAsync(CancellationToken ct = default);
    }
}
