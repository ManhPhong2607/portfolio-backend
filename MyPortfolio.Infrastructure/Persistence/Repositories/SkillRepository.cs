using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces.Repositories;

namespace MyPortfolio.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _context;
        public SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Skill>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Skills
                .OrderBy(s => s.Category)
                .ThenBy(s => s.DisplayOrder)
                .ToListAsync(ct);
        }

        public async Task<Skill?> GetByIdAsync( Guid id, CancellationToken ct = default)
        {
            return await _context.Skills.FirstOrDefaultAsync(s => s.Id == id, ct);
        }

        public async Task AddAsync(Skill skill, CancellationToken ct = default)
        {
            await _context.Skills.AddAsync(skill, ct);
        }

        public Task UpdateAsync(Skill skill, CancellationToken ct = default)
        {
            _context.Skills.Update(skill);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Skill skill, CancellationToken ct = default)
        {
            _context.Skills.Remove(skill);
            return Task.CompletedTask;
        }
    }
}
