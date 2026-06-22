using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Domain.Interfaces.Repositories
{
    public interface ISkillRepository 
    {
        Task<List<Skill>> GetAllAsync(CancellationToken ct = default);
        Task<Skill?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Skill skill, CancellationToken ct = default);
        Task UpdateAsync(Skill skill, CancellationToken ct = default);
        Task DeleteAsync(Skill skill, CancellationToken ct = default);
    }
}
