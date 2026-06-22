using Microsoft.EntityFrameworkCore;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Enums;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository(ApplicationDbContext context) : IProjectRepository
    {
        public async Task<PaginatedResult<Project>> GetPublishedAsync(int page, int limit, Guid? technologyId = null, CancellationToken ct = default)
        {
            var query = context.Projects
                .Include(p => p.Technologies)
                .Include(p => p.ThumbnailMedia)
                .Where(p => p.Status == ProjectStatus.InProgress
                         || p.Status == ProjectStatus.Completed )
                .AsQueryable();
            if(technologyId.HasValue)
                query = query.Where(p=> p.Technologies.Any(t=> t.Id == technologyId.Value)); 

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(p=> p.DisplayOrder)
                .ThenByDescending(p=> p.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return new PaginatedResult<Project>(items, total, page, limit); 
        }

        public async Task<Project?> GetBySlugAsync(string slug, CancellationToken ct = default)
        {
           return await context.Projects
                .Include(p=> p.Technologies)
                .Include(p=> p.ThumbnailMedia)
                .FirstOrDefaultAsync(p=> p.Slug.Value == slug, ct);
        }

        public async Task<PaginatedResult<Project>> GetAllAsync(int page, int limit, ProjectStatus? status = null, CancellationToken ct = default)
        {
            var query = context.Projects
                .Include(p=> p.Technologies)
                .Include(p=> p.ThumbnailMedia)
                .AsQueryable();

            if(status != null)
                query = query.Where(p=> p.Status == status.Value);

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderBy(p=> p.DisplayOrder)
                .ThenByDescending(p=> p.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(ct);
            return new PaginatedResult<Project>(items,total,page,limit);
        }

        public async Task<Project?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await context.Projects
                .Include(p=> p.Technologies)
                .Include(p=> p.ThumbnailMedia)
                .FirstOrDefaultAsync(p=> p.Id == id, ct);
        }

        public async Task<bool> SlugExistAsync(string slug, Guid? excludedId = null, CancellationToken ct = default)
        {
            var query = context.Projects.Where(p=> p.Slug.Value == slug);
            if(excludedId.HasValue)
                query = query.Where(p=> p.Id != excludedId.Value);
            return await query.AnyAsync(ct);
        }

        public async Task AddAsync(Project project, CancellationToken ct = default)
        {
            await context.Projects.AddAsync(project, ct);
        }

        public Task UpdateAsync(Project project, CancellationToken ct = default)
        {
            context.Projects.Update(project);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Project project, CancellationToken ct = default)
        {
            context.Projects.Remove(project);
            return Task.CompletedTask;
        }     
       
        public async Task ReorderAsync(List<(Guid Id, int order)> orders, CancellationToken ct = default)
        {
            foreach (var (id, order) in orders)
            {
                await context.Projects.Where(p => p.Id == id)
                    .ExecuteUpdateAsync(s => s.SetProperty(p => p.DisplayOrder, order), ct);
            }
        }             
    }
}
