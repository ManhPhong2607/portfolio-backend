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
    public class TechnologyRepository(ApplicationDbContext context) : ITechnologyRepository
    {
        public async Task<List<Technology>> GetAllAsync(CancellationToken ct = default)
        {
            return await context.Technologies
                .Include(t=> t.Projects)
                .OrderBy(t => t.Name)
                .ToListAsync(ct);
        }
     

        public async Task<Technology?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            //return await context.Technologies.FindAsync([id], ct);
            return await context.Technologies.FirstOrDefaultAsync(t => t.Id == id, ct);
        }
           

        public async Task<bool> IsUsedAsync(Guid id, CancellationToken ct = default)
        {
            return await context.Projects
                .AnyAsync(p => p.Technologies.Any(t => t.Id == id), ct);
        }
           

        public async Task AddAsync(Technology technology, CancellationToken ct = default)
        {
            await context.Technologies.AddAsync(technology, ct);
        }
           

        public Task UpdateAsync(Technology technology, CancellationToken ct = default)
        {
            context.Technologies.Update(technology);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Technology technology, CancellationToken ct = default)
        {
            context.Technologies.Remove(technology);
            return Task.CompletedTask;
        }

        public async Task<Dictionary<Guid, int>> GetProjectCountsAsync(CancellationToken ct = default)
        {
            return await context.Technologies
                .Select(t => new { t.Id, ProjectCount = t.Projects.Count })
                .ToDictionaryAsync(x => x.Id, x => x.ProjectCount, ct);
        }
    }
}
