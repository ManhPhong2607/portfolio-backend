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
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tag tag, CancellationToken ct = default)
        {
            await _context.Tags.AddAsync(tag, ct);
        }

        public Task DeleteAsync(Tag tag, CancellationToken ct = default)
        {
            _context.Tags.Remove(tag);
            return Task.CompletedTask;
        }

        public async Task<List<Tag>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Tags
               .OrderBy(t => t.Name)
               .ToListAsync(ct);
        }

        public async Task<Tag?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.Id == id, ct);
        }

        public async Task<Tag?> GetBySlugAsync(string slug, CancellationToken ct = default)
        {
            return await _context.Tags
                .FirstOrDefaultAsync(t => t.Slug.Value == slug, ct);
        }

        public Task UpdateAsync(Tag tag, CancellationToken ct = default)
        {
            _context.Tags.Update(tag);
            return Task.CompletedTask;
        }

        public async Task<Dictionary<Guid, int>> GetBlogCountsAsync(CancellationToken ct = default)
        {
            return await _context.Tags
                .Select(t => new { t.Id, BlogCount = t.BlogPosts.Count })
                .ToDictionaryAsync(x => x.Id, x => x.BlogCount, ct);
        }

        //public async Task<bool> IsUsedAsync(Guid id, CancellationToken ct = default)
        //{
        //     return await _context.BlogPosts
        //    .AnyAsync(p => p.Tags.Any(t => t.Id == id), ct);
        //}

    }
}
