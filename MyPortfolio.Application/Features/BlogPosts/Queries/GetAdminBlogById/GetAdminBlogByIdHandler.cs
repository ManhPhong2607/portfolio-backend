using MediatR;
using MyPortfolio.Application.Features.BlogPosts.DTOs;
using MyPortfolio.Application.Features.BlogPosts.Queries.GetAdminBlogById;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Application.Features.Tags.DTOs;
namespace MyPortfolio.Application.Features.BlogPosts.Queries.GetAdminBlogById
{
    public class GetAdminBlogByIdHandler(IBlogPostRepository blogpostrepository)
        : IRequestHandler<GetAdminBlogByIdQuery, BlogPostDetailDto>
    {
        public async Task<BlogPostDetailDto> Handle(GetAdminBlogByIdQuery request, CancellationToken ct)
        {
            var post = await blogpostrepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Bài viết {request.Id} không tồn tại.");

            var adjacent = await blogpostrepository.GetAdjacentAsync(post.Id, ct);
            return new BlogPostDetailDto(
                 Id: post.Id,
                 Title: post.Title,
                 Slug: post.Slug.Value,
                 Excerpt: post.Excerpt,
                 Content: post.Content,
                 CoverImageUrl: post.CoverMedia?.SecureUrl,
                 CoverMediaId: post.CoverMediaId,
                 Status: post.Status,
                 ReadingTimeMinutes: post.ReadingTimeMinutes,
                 ViewCount: post.ViewCount,
                 PublishedAt: post.PublishedAt,
                 CreatedAt: post.CreatedAt,
                 UpdatedAt: post.UpdateAt,
                 Tags: post.Tags.Select(t => new BlogTagDto(t.Id, t.Name, t.Slug.Value)).ToList(),
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
