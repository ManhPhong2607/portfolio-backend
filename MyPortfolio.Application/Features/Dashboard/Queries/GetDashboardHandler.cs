using MediatR;
using MyPortfolio.Application.Features.Dashboard.DTOs;
using MyPortfolio.Domain.Enums;
using MyPortfolio.Domain.Interfaces.Repositories;
using System.Linq;

namespace MyPortfolio.Application.Features.Dashboard.Queries
{
    public class GetDashboardHandler(
         IBlogPostRepository blogRepository,
         IProjectRepository projectRepository
      // IContactMessageRepository contactRepository
    ) : IRequestHandler<GetDashboardQuery, DashboardDto>
    {
        public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken ct)
        {
            var blogTask = await blogRepository.GetAllAsync(1, int.MaxValue, ct: ct);
            var projectTask = await projectRepository.GetAllAsync(1, int.MaxValue, ct: ct);
            //var unreadTask = contactRepository.CountUnreadAsync(ct);


            var allPosts = blogTask.Items;
            var allProjects = projectTask.Items;
            //var unread = unreadTask.Result;

            var totalBlogPosts = allPosts.Count();
            var totalPublishedPosts = allPosts.Count(p => p.Status == PostStatus.Published);
            var draftPosts = allPosts.Count(p => p.Status == PostStatus.Draft);
            var totalProjects = allProjects.Count();
            var draftProjects = allProjects.Count(p => p.Status == ProjectStatus.Draft);

            var topViewedPosts = allPosts
                .Where(p => p.Status == PostStatus.Published)
                .OrderByDescending(p => p.ViewCount)
                .Take(5)
                .Select(p => new DashboardPostDto(
                    p.Id,
                    p.Title,
                    p.Slug.Value,
                    p.Status.ToString(),
                    p.ViewCount,
                    p.PublishedAt,
                    p.UpdateAt
                ))
                .ToList();

            var recentPosts = allPosts
                .OrderByDescending(x => x.UpdateAt)
                .Take(5)
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
              
            var recentProjects = allProjects
              .OrderByDescending(p => p.UpdateAt)
              .Take(5)
              .Select(p => new DashboardProjectDto(
                  Id: p.Id,
                  Title: p.Title,
                  Slug: p.Slug.Value,
                  Status: p.Status.ToString(),
                  IsFeatured: p.IsFeatured,
                  UpdatedAt: p.UpdateAt
              ))
              .ToList();

            return new DashboardDto(
                 TotalBlogPosts: totalBlogPosts,
                 TotalPublishedPosts: totalPublishedPosts,
                 DraftPosts: draftPosts,
                 TotalProjects: totalProjects,
                 TotalDraftProjects: draftProjects,
                 TopViewedPosts: topViewedPosts,
                 RecentPosts: recentPosts,
                 RecentProjects: recentProjects
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