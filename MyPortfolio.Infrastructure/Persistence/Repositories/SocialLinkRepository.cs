using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Infrastructure.Persistence.Repositories
{
    public class SocialLinkRepository : ISocialLinkRepository
    {
        private readonly ApplicationDbContext _context;

        public SocialLinkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SocialLink socialLink, CancellationToken ct = default)
        {
             await _context.SocialLinks.AddAsync(socialLink, ct);
        }

        public Task DeleteAsync(SocialLink socialLink, CancellationToken ct = default)
        {
            _context.SocialLinks.Remove(socialLink);
            return Task.CompletedTask;
        }

        public async Task<List<SocialLink>> GetAllAsync(CancellationToken ct = default)
        {
           return await _context.SocialLinks
                //.Where(s=>s.IsVisible)
                .OrderBy(s=> s.DisplayOrder)
                .ToListAsync();
        }

        public async Task<SocialLink?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.SocialLinks.FirstOrDefaultAsync(s => s.Id == id, ct);
        }

        public async Task<List<SocialLink>> GetVisibleAsync(CancellationToken ct = default)
        {
            return await _context.SocialLinks
                .Where(s=> s.IsVisible)
                .OrderBy(s=> s.DisplayOrder) .ToListAsync(ct);
        }

        public Task UpdateAsync(SocialLink socialLink, CancellationToken ct = default)
        {
            _context.SocialLinks.Update(socialLink);
            return Task.CompletedTask;
        }
    }
}
