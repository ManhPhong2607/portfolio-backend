using MyPortfolio.Application.Features.Tags.DTOs;
using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.DTOs
{
    public record BlogPostSummaryDto(
        Guid Id,
        string Title,
        string Slug,
        string? Excerpt,
        string? CoverImageUrl,
        PostStatus Status,
        int ReadingTimeMinutes,
        int ViewCount,
        DateTime? PublishedAt,
        DateTime CreatedAt,
        List<BlogTagDto> Tags
        );   
}
