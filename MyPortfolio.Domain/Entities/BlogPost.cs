using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.ValueObjects;
using MyPortfolio.Domain.Enums;


namespace MyPortfolio.Domain.Entities
{
    public class BlogPost : BaseEntity
    {
        public Guid AuthorId { get; private set; }
        public string Title { get; private set; } = default;
        public Slug Slug { get; private set; } = default;
        public string? Excerpt { get; private set; }
        public string Content { get; private set; } = default;
        public Guid? CoverMediaId { get; private set; }
        public virtual MediaFile? CoverMedia { get; private set; }
        public PostStatus Status { get; private set; }
        public int ReadingTimeMinutes { get; private set; }
        public int ViewCount { get; private set; }
        public DateTime? PublishedAt { get; private set; }

        public User Author { get; private set; } = default!;
        public virtual ICollection<Tag> Tags { get; private set; } = new List<Tag>();

        private BlogPost() { }

        public static BlogPost Create(Guid authorId, string title, string content, string? excerpt = null,
            Guid? coverMediaId = null)
        {
            if(string.IsNullOrWhiteSpace(title))
                throw new DomainException("Tiêu đề không được rỗng");

            if(string.IsNullOrWhiteSpace(content))
                throw new DomainException("Nội dung không được rỗng");

            return new BlogPost
            {
                AuthorId = authorId,
                Title = title.Trim(),
                Slug = Slug.Create(title),
                Content = content.Trim(),
                Excerpt = excerpt?.Trim(),
                CoverMediaId = coverMediaId,
                Status = PostStatus.Draft,
                ReadingTimeMinutes = CalculateReadingTime(content),
                ViewCount = 0
            };
        }

        public void Update(string title, string content, string? excerpt = null, Guid? coverMediaId = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Tiêu đề không được rỗng");

            if (string.IsNullOrWhiteSpace(content))
                throw new DomainException("Nội dung không được rỗng");

            Title = title.Trim();
            Slug = Slug.Create(title);
            Content = content.Trim();
            Excerpt = excerpt?.Trim();
            CoverMediaId = coverMediaId;
            ReadingTimeMinutes = CalculateReadingTime(content);
            SetUpdated();
        }

        public void Publish()
        {
            if (Status == PostStatus.Published)
                throw new DomainException("Bài viết đã được publish");

            if(string.IsNullOrWhiteSpace(Content))
                throw new DomainException("Không thể publish bài viết rỗng");

            Status = PostStatus.Published;
            PublishedAt = DateTime.UtcNow;
            SetUpdated();
        }

        public void Unpublish()
        {
            //if (Status == PostStatus.Draft)
            //    throw new DomainException("Bài viết đã ở trạng thái draft");

            Status = PostStatus.Draft;
            SetUpdated();
        }

        public void Archive() 
        {
            Status = PostStatus.Archived;
            SetUpdated();
        }

        public void IncrementViewCount()
        {
            ViewCount++;
        }

        public void SetTags(IEnumerable<Tag> tags)
        {
            Tags.Clear();
            foreach (var tag in tags)
            {
                Tags.Add(tag);
            }
            SetUpdated();
        }

        private static int CalculateReadingTime(string content)
        {
            var wordCount = content
                .Split([' ', '\n', '\r', '\t'], StringSplitOptions.RemoveEmptyEntries)
                .Length;
            return Math.Max(1, (int)Math.Ceiling(wordCount / 200.0));

        }
    }
}
