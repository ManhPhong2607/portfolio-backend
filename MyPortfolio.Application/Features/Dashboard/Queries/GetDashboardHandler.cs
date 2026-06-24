using MediatR;
using MyPortfolio.Application.Features.Dashboard.DTOs;
using MyPortfolio.Domain.Enums;
using MyPortfolio.Domain.Interfaces.Repositories;
using System.Linq;

namespace MyPortfolio.Application.Features.Dashboard.Queries
{
    public class GetDashboardHandler(
         IBlogPostRepository blogRepository,
         IProjectRepository projectRepository,
         IContactMessageRepository messageRepository
    ) : IRequestHandler<GetDashboardQuery, DashboardDto>
    {
        public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken ct)
        {
            var blogTask = await blogRepository.GetAllAsync(1, int.MaxValue, ct: ct);
            var projectTask = await projectRepository.GetAllAsync(1, int.MaxValue, ct: ct);
            var unreadTask = await messageRepository.CountUnreadAsync(ct);
            var messageTask = await messageRepository.GetAllAsync(1,3,ct:ct);

            var allPosts = blogTask.Items;
            var allProjects = projectTask.Items;
            var recentMsgs = messageTask.Items;

            //stats
            var totalBlogPosts = allPosts.Count();
            var totalPublishedPosts = allPosts.Count(p => p.Status == PostStatus.Published);
            var draftPosts = allPosts.Count(p => p.Status == PostStatus.Draft);
            var totalProjects = allProjects.Count();
            var draftProjects = allProjects.Count(p => p.Status == ProjectStatus.Draft);
            var totalViews = allPosts.Sum(p => p.ViewCount);

            //3 bai moi nhat
            var recentPosts = allPosts
                .OrderByDescending(x => x.UpdateAt)
                .Take(3)
                .Select(x => new DashboardPostDto(
                    x.Id,
                    x.Title,
                    x.Slug.Value,
                    x.Status.ToString(),
                    x.ViewCount,
                    x.PublishedAt,
                    x.UpdateAt
                ))
                .ToList();

            //3 tin moi nhat
            var recentMessages = recentMsgs
                .Select(m => new DashboardMessageDto(
                 Id: m.Id,
                 SenderName: m.SenderName,
                 SenderEmail: m.SenderEmail.Value,
                 Subject: m.Subject,
                 Status: m.Status.ToString(),
                 SentAt: m.SentAt
            )).ToList();
        

            return new DashboardDto(
                 TotalBlogPosts: totalBlogPosts,
                 TotalPublishedPosts: totalPublishedPosts,
                 DraftPosts: draftPosts,
                 TotalProjects: totalProjects,
                 TotalDraftProjects: draftProjects,
                 TotalViews: totalViews,
                 TotalUnreadMessages: unreadTask,
                 RecentPosts: recentPosts,
                 RecentMessages: recentMessages
            );
        }

        //private static DashboardPostDto MapToPostDto(Domain.Entities.BlogPost p) =>
        //new(
        //    Id: p.Id,
        //    Title: p.Title,
        //    Slug: p.Slug.Value,
        //    Status: p.Status.ToString(),
        //    ViewCount: p.ViewCount,
        //    PublishedAt: p.PublishedAt,
        //    UpdatedAt: p.UpdateAt 
        //);
    }
}