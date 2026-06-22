using MyPortfolio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Entities
{
    public class SocialLink : BaseEntity
    {
        public string Platform { get; private set; }
        public string? Label { get; private set; } // ten hien thi tuy chinh
        public string Url { get; private set; }
        public string? IconUrl { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool IsVisible { get; private set; }

        private SocialLink() { }

        private static readonly HashSet<string> AllowedPlatforms = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "github", "facebook", "instagram",
            "youtube", "tiktok", "twitter", "email"
        };


        public static SocialLink Create(string platform, string url, string? IconUrl, string? label = null, bool isVisible = true)
        {
            var p = platform.ToLowerInvariant().Trim();
            if (!AllowedPlatforms.Contains(p))
                throw new DomainException($"Platform '{platform}' không được hỗ trợ.");

            if (string.IsNullOrWhiteSpace(url))
                throw new DomainException("URL không được rỗng.");

            return new SocialLink
            {
                Platform = p,
                Url = url.Trim(),
                Label = label?.Trim(),
                IconUrl = IconUrl?.Trim(),
                IsVisible = true
            };
        }

        public void Update(string url, string? label, string? iconUrl, bool isVisible)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new DomainException("URL không được rỗng.");

            Url = url.Trim();
            Label = label?.Trim();
            IconUrl = iconUrl?.Trim();
            IsVisible = isVisible;
            SetUpdated();
        }

        public void ToggleVisibility()
        {
            IsVisible = !IsVisible;
            SetUpdated();
        }

        public void SetOrder(int order)
        {
            DisplayOrder = order;
            SetUpdated();
        }
    }

}

