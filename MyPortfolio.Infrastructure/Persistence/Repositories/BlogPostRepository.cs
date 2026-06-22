using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;
        public BlogPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public
        public async Task<PaginatedResult<BlogPost>> GetPublishedAsync(int page, int limit, string? tagSlug = null, string? search = null, CancellationToken ct = default)
        {
            var query = _context.BlogPosts
                .Include(b=>b.Tags)
                .Include(b=>b.CoverMedia)
                .Where(b=> b.Status == PostStatus.Published)
                .AsQueryable();
            if(!string.IsNullOrWhiteSpace(tagSlug))
                query = query.Where(b=> b.Tags.Any(t => t.Slug.Value == tagSlug));
            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchTerm = search.Trim().ToLower();

                query = query.Where(b =>
                    b.Title.ToLower().Contains(searchTerm) ||
                    (b.Excerpt != null && b.Excerpt.ToLower().Contains(searchTerm))
                );
            }
            //if(!string.IsNullOrWhiteSpace(search))
            //    query = query.Where(b=> b.Title.Contains(search) || 
            //    b.Excerpt != null && b.Excerpt.Contains(search));
            var total = await query.CountAsync(ct);
            var items = await query
                .OrderByDescending(b=> b.PublishedAt)
                .Skip((page -1) * limit)
                .Take(limit)
                .ToListAsync(ct);
            return new PaginatedResult<BlogPost>(items, total, page, limit);
        }

        public async Task<BlogPost?> GetBySlugAsync(string slug, CancellationToken ct = default)
        {
            return await _context.BlogPosts
                .Include(b => b.Tags)
                .Include(b => b.CoverMedia)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Slug.Value == slug, ct);
        }

        public async Task<AdjacentPosts> GetAdjacentAsync(Guid postId, CancellationToken ct = default)
        {
            var current = await _context.BlogPosts
                .FirstOrDefaultAsync(b=> b.Id == postId, ct);
            if(current?.PublishedAt == null)
                return new AdjacentPosts(null, null, null, null, null, null);
            var prev = await _context.BlogPosts
                .Where(b => b.Status == PostStatus.Published && b.PublishedAt < current.PublishedAt)
                .OrderByDescending(b => b.PublishedAt)
                .Select(b => new { b.Id, b.Title, Slug = b.Slug.Value })
                .FirstOrDefaultAsync(ct);

            var next = await _context.BlogPosts
                .Where(b => b.Status == PostStatus.Published && b.PublishedAt > current.PublishedAt)
                .OrderBy(b => b.PublishedAt)
                .Select(b => new { b.Id, b.Title, Slug = b.Slug.Value })
                .FirstOrDefaultAsync(ct);

            return new AdjacentPosts(
                PrevId: prev?.Id,
                PrevTitle: prev?.Title,
                PrevSlug: prev?.Slug,
                NextId: next?.Id,
                NextTitle: next?.Title,
                NextSlug: next?.Slug
             );
        }

        //admin     
        public async Task<PaginatedResult<BlogPost>> GetAllAsync(int page, int limit, PostStatus? status = null, CancellationToken ct = default)
        {
            var query = _context.BlogPosts
                .Include(b=>b.Tags)
                .Include(b=>b.CoverMedia)
                .AsQueryable();
            if (status.HasValue)
                query = query.Where(b => b.Status == status.Value);        

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderByDescending(b => b.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(ct);
            return new PaginatedResult<BlogPost>(items, total, page, limit);
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.BlogPosts
                .Include(b => b.Tags)
                .Include(b => b.CoverMedia)
                .FirstOrDefaultAsync(b => b.Id == id, ct);
        }    

        public async Task IncrementViewCountAsync(Guid id, CancellationToken ct = default)
        {
             await _context.BlogPosts
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(b => b.ViewCount, b => b.ViewCount + 1), ct);
        }

        public async Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken ct = default)
        {
           var query = _context.BlogPosts
                .Where(b=> b.Slug.Value == slug);
            if(excludeId.HasValue)
                query = query.Where(b => b.Id != excludeId.Value);
            return await query.AnyAsync(ct);
        }

        public async Task AddAsync(BlogPost post, CancellationToken ct = default)
        {
           await _context.BlogPosts.AddAsync(post, ct);
        }

        public Task DeleteAsync(BlogPost post, CancellationToken ct = default)
        {
            _context.BlogPosts.Remove(post);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(BlogPost post, CancellationToken ct = default)
        {
            _context.BlogPosts.Update(post);
            return Task.CompletedTask;
        }
    }
}
