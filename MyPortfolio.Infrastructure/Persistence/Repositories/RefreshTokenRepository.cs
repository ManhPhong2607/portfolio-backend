using Microsoft.EntityFrameworkCore;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;
        public RefreshTokenRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(ReFreshToken token, CancellationToken ct = default)
        {
            await _context.RefreshTokens.AddAsync(token, ct);
        }

        public async Task<ReFreshToken?> GetByTokenAsync(string token, CancellationToken ct = default)
        {
            return await _context.RefreshTokens.Include(r =>r.User)
                .FirstOrDefaultAsync(r=> r.Token == token, ct);
        }

        public async Task RevokeAllByUserAsync(Guid userId, CancellationToken ct = default)
        {
             await _context.RefreshTokens.Where(r => r.UserId == userId && !r.IsRevoked)
                 .ExecuteUpdateAsync(s => s.SetProperty(r => r.IsRevoked, true), ct);
        }

        public async Task UpdateAsync(ReFreshToken token, CancellationToken ct = default)
        {
            _context.RefreshTokens.Update(token);
        }
    }
}
