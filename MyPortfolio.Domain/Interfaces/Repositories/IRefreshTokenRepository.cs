using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Entities;
namespace MyPortfolio.Domain.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<ReFreshToken?> GetByTokenAsync(string token, CancellationToken ct =default );
        Task AddAsync(ReFreshToken token, CancellationToken ct = default);
        Task UpdateAsync(ReFreshToken token, CancellationToken ct = default);
        Task RevokeAllByUserAsync(Guid userId, CancellationToken ct = default);

    }
}
