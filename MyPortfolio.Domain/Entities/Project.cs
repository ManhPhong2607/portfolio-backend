
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Enums;
using MyPortfolio.Domain.ValueObjects;
using System.Security.Cryptography;
using System.Xml;

namespace MyPortfolio.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Title { get; private set; } = default!;
        public Slug Slug { get; private set; } = default!;
        public string? ShortDescription { get; private set; }
        public string? DetailContent { get; private set; }
        public string? DemoUrl { get; private set; }
        public string? GithubUrl { get; private set; }
        public ProjectStatus Status { get; private set; }
        public int DisplayOrder { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsFeatured { get; private set; }

        public Guid? ThumbnailMediaId { get; private set; }
        public virtual MediaFile? ThumbnailMedia { get; private set; }

        public ICollection<Technology> Technologies { get; private set; }
         = new List<Technology>();

        private Project() { }

        public static Project Create(string title, string? shortDescription = null,
            string? detailContent = null, string? demoUrl = null, string? githubUrl = null,
            DateTime? startDate = null, DateTime? endDate = null, Guid? thumbnailMediaId = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Tiêu đề project không được rỗng.");

            return new Project
            {
                Title = title.Trim(),
                Slug = Slug.Create(title),
                ShortDescription = shortDescription?.Trim(),
                DetailContent = detailContent,
                DemoUrl = demoUrl?.Trim(),
                GithubUrl = githubUrl?.Trim(),
                StartDate = startDate.HasValue
                   ? DateTime.SpecifyKind(startDate.Value.Date, DateTimeKind.Utc)
                   : null,
                EndDate = endDate.HasValue
                   ? DateTime.SpecifyKind(endDate.Value.Date, DateTimeKind.Utc)
                   : null,
                ThumbnailMediaId = thumbnailMediaId,
                Status = ProjectStatus.Draft
            };
        }

        public void Update(string title, string? shortDescription, string? detailContent,
            string? demoUrl, string? githubUrl, DateTime? startDate,
            DateTime? endDate, Guid? thumbnailMediaId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Tiêu đề project không được rỗng.");

            if (endDate.HasValue && startDate.HasValue && endDate.Value < startDate.Value)
                throw new DomainException("EndDate phải sau StartDate.");

            Title = title.Trim();
            Slug = Slug.Create(title);
            ShortDescription = shortDescription?.Trim();
            DetailContent = detailContent;
            DemoUrl = demoUrl?.Trim();
            GithubUrl = githubUrl?.Trim();
            StartDate = startDate.HasValue
                ? DateTime.SpecifyKind(startDate.Value.Date, DateTimeKind.Utc)
                : null;
            EndDate = endDate.HasValue
                ? DateTime.SpecifyKind(endDate.Value.Date, DateTimeKind.Utc)
                : null;
            ThumbnailMediaId = thumbnailMediaId;
            SetUpdated();
        }

        public void SetTechnologies(IEnumerable<Technology> technologies)
        {
            Technologies.Clear();
            foreach (var tech in technologies)
            {
                Technologies.Add(tech);
            }
            SetUpdated();
        }

        public void ToggleFeatured()
        {
            IsFeatured = !IsFeatured;
            SetUpdated();
        }

        public void SetOrder(int order)
        {
            DisplayOrder = order;
            //if (order < 0)
            //    throw new DomainException("DisplayOrder phải >= 0.");
            SetUpdated();
        }

        public void ChangeStatus(ProjectStatus newStatus)
        {
            // Không cho phép từ Archived quay lại Draft
            //if (Status == ProjectStatus.Archived && newStatus == ProjectStatus.Draft)
            //    throw new DomainException("Không thể chuyển từ Archived về Draft.");

            Status = newStatus;
            SetUpdated();
        }
    }
}
