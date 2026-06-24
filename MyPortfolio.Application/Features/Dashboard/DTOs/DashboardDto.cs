using MyPortfolio.Application.Features.BlogPosts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Dashboard.DTOs
{
    public record DashboardDto(
        int TotalBlogPosts,
        int TotalPublishedPosts,
        int DraftPosts,
        int TotalProjects,
        int TotalDraftProjects,
        int TotalUnreadMessages,
        int TotalViews,

        List<DashboardPostDto> RecentPosts,      // 3 bài tạo/sửa mới nhất
        List<DashboardMessageDto> RecentMessages
    );

    public record DashboardPostDto(
        Guid Id,
        string Title,
        string Slug,
        string Status,
        int ViewCount,
        DateTime? PublishedAt,
        DateTime UpdatedAt
    );

    //public record DashboardProjectDto(
    //    Guid Id,
    //    string Title,
    //    string Slug,
    //    string Status,
    //    bool IsFeatured,
    //    DateTime UpdatedAt
    //);

    public record DashboardMessageDto(
        Guid Id,
        string SenderName,
        string SenderEmail,
        string? Subject,
        string Status,
        DateTime SentAt
    );
}
