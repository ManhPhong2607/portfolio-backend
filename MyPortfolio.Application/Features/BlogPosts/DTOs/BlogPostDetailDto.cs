using MyPortfolio.Application.Features.Tags.DTOs;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.DTOs
{
    public record BlogPostDetailDto(
        Guid Id,
        string Title,
        string Slug,
        string Content,
        string? Excerpt,
        string? CoverImageUrl,
        Guid? CoverMediaId,
        PostStatus Status,
        int ReadingTimeMinutes,
        int ViewCount,
        DateTime? PublishedAt,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        List<BlogTagDto> Tags,
        AdjacentPostsDto? Prev,
        AdjacentPostsDto? Next
        );

    public record AdjacentPostsDto(Guid Id, string Title, string Slug);
    
}
