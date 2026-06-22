using MediatR;
using MyPortfolio.Application.Features.BlogPosts.DTOs;
using MyPortfolio.Application.Features.Tags.DTOs;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Queries.GetBlogList
{
    public class GetBlogListHandler(IBlogPostRepository repository)
        : IRequestHandler<GetBlogListQuery, PaginatedResult<BlogPostSummaryDto>>
    {
        public async Task<PaginatedResult<BlogPostSummaryDto>> Handle(GetBlogListQuery request, CancellationToken ct)
        {
            var result = await repository.GetPublishedAsync(
                request.Page, request.Limit,
                request.TagSlug, request.Search, ct);
            var dtos = result.Items.Select(b => new BlogPostSummaryDto(
                Id: b.Id,
                Title: b.Title,
                Slug: b.Slug,
                Excerpt: b.Excerpt,
                CoverImageUrl: b.CoverMedia?.SecureUrl,
                Status: b.Status,
                ReadingTimeMinutes: b.ReadingTimeMinutes,
                ViewCount: b.ViewCount,
                PublishedAt: b.PublishedAt,
                CreatedAt: b.CreatedAt,
                Tags: b.Tags.Select(t => new BlogTagDto
                (
                    t.Id,
                    t.Name,
                    t.Slug.Value
                )).ToList()
            )).ToList();

            return new PaginatedResult<BlogPostSummaryDto>(
                dtos, result.TotalCount, result.Page, result.Limit);
        }
    }
}
