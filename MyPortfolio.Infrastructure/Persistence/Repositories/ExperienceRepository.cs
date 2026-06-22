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
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly ApplicationDbContext _context;

        public ExperienceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Experience experience, CancellationToken ct = default)
        {
            await _context.Experiences.AddAsync(experience, ct);
        }

        public  Task DeleteAsync(Experience experience, CancellationToken ct = default)
        {
            _context.Experiences.Remove(experience);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Experience experience, CancellationToken ct = default)
        {
            _context.Experiences.Update(experience);
            return Task.CompletedTask;
        }

        public async Task<List<Experience>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Experiences
                .OrderBy(e=> e.DisplayOrder)
                .ThenByDescending(e=> e.StartDate)
                .ToListAsync(ct);
        }

        public async Task<Experience?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Experiences.FindAsync([id], ct);
            //return await _context.Experiences.FirstOrDefaultAsync(e=>e.Id == id, ct);
        }

        
    }
}
