using MyPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Enums;
namespace MyPortfolio.Domain.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<PaginatedResult<Project>> GetPublishedAsync(
            int page, int limit, Guid? technologyId = null, CancellationToken ct = default);
        Task<Project?> GetBySlugAsync(string slug, CancellationToken ct = default);

        Task<Project?> GetByIdAsync(Guid id, CancellationToken ct = default);        
        Task<PaginatedResult<Project>> GetAllAsync(
            int page, int limit, ProjectStatus? status = null, CancellationToken ct = default);
        Task<bool> SlugExistAsync(string slug, Guid? excludedId = null, CancellationToken ct = default);
        Task AddAsync(Project project, CancellationToken ct = default);
        Task UpdateAsync(Project project, CancellationToken ct = default);
        Task DeleteAsync(Project project, CancellationToken ct = default);
        Task ReorderAsync(List<(Guid Id, int order)> orders, CancellationToken ct = default);
    }
}
