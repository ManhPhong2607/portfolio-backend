using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Domain.Interfaces.Repositories
{
    public interface IBlogPostRepository
    {
        //public 
        Task<PaginatedResult<BlogPost>> GetPublishedAsync(
            int page, int limit,
            string? tagSlug = null,
            string? search = null,
            CancellationToken ct = default);

        Task<BlogPost?> GetBySlugAsync(string slug, CancellationToken ct = default);
        Task<AdjacentPosts> GetAdjacentAsync(Guid postId, CancellationToken ct = default);

        //admin
        Task<PaginatedResult<BlogPost>> GetAllAsync(
        int page, int limit,
        PostStatus? status = null,
        CancellationToken ct = default);

        Task<BlogPost?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken ct = default);

        Task AddAsync(BlogPost post, CancellationToken ct = default);
        Task UpdateAsync(BlogPost post, CancellationToken ct = default);
        Task DeleteAsync(BlogPost post, CancellationToken ct = default);
        Task IncrementViewCountAsync(Guid id, CancellationToken ct = default);
    }
   
}
