using MyPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Interfaces.Repositories
{
    public interface ISocialLinkRepository
    {
        Task<List<SocialLink>> GetAllAsync(CancellationToken ct = default);
        Task<List<SocialLink>> GetVisibleAsync(CancellationToken ct = default); //cho public chi thay true
        Task<SocialLink?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(SocialLink socialLink, CancellationToken ct = default);
        Task UpdateAsync(SocialLink socialLink, CancellationToken ct = default);
        Task DeleteAsync(SocialLink socialLink, CancellationToken ct = default);
    }
}
