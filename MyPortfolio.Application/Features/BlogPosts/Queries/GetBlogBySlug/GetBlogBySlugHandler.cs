using MediatR;
using MyPortfolio.Application.Features.BlogPosts.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Enums;
using MyPortfolio.Application.Features.Tags.DTOs;
namespace MyPortfolio.Application.Features.BlogPosts.Queries.GetBlogBySlug
{
    public class GetBlogBySlugHandler(IBlogPostRepository blogpostrepository) : IRequestHandler<GetBlogBySlugQuery, BlogPostDetailDto>
    {
        public async Task<BlogPostDetailDto> Handle(GetBlogBySlugQuery request, CancellationToken ct)
        {
            var post = await blogpostrepository.GetBySlugAsync(request.Slug, ct)
                ?? throw new NotFoundException($"bài viết '{request.Slug}' không tồn tại.");

            if(post.Status != PostStatus.Published)
                throw new NotFoundException($"bài viết '{request.Slug}' không tồn tại.");
            var adjacent = await blogpostrepository.GetAdjacentAsync(post.Id, ct);

            return new BlogPostDetailDto(
                Id: post.Id,
                Title: post.Title,
                Slug: post.Slug.Value,
                Excerpt: post.Excerpt,
                Content: post.Content,
                CoverImageUrl: post.CoverMedia?.SecureUrl,
                Status: post.Status,
                ReadingTimeMinutes: post.ReadingTimeMinutes,
                ViewCount: post.ViewCount,
                PublishedAt: post.PublishedAt,
                CreatedAt: post.CreatedAt,
                UpdatedAt: post.UpdateAt,
                Tags: post.Tags.Select(t => new BlogTagDto
                (
                    t.Id,
                    t.Name,
                    t.Slug.Value                 
                )).ToList(),
                Prev: adjacent.PrevId.HasValue
                    ? new AdjacentPostsDto(adjacent.PrevId.Value, adjacent.PrevTitle!, adjacent.PrevSlug!)
                    : null,
                Next: adjacent.NextId.HasValue
                    ? new AdjacentPostsDto(adjacent.NextId.Value, adjacent.NextTitle!, adjacent.NextSlug!)
                    : null
             );
        }
    }
}
