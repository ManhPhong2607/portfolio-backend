using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Entities;
namespace MyPortfolio.Domain.Interfaces.Repositories
{
    public interface IExperienceRepository
    {
        Task<List<Experience>> GetAllAsync(CancellationToken ct = default);
        Task<Experience?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Experience experience, CancellationToken ct= default);
        Task UpdateAsync(Experience experience, CancellationToken ct= default);
        Task DeleteAsync(Experience experience, CancellationToken ct= default);
    }
}
