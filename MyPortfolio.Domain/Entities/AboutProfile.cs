using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.ValueObjects;
namespace MyPortfolio.Domain.Entities
{
    public class AboutProfile : BaseEntity
    {
        public string FullName { get; private set; }
        public string? Tagline { get; private set; }
        public string? Bio { get; private set; }
        public string? Location { get; private set; }
        public Email? ContactEmail { get; private set; }
        public Guid? AvatarMediaId { get; private set; }
        public Guid? CvMediaId { get; private set; }
        public virtual MediaFile? Avatar { get; private set; }
        public virtual MediaFile? CvFile { get; private set; }

        private AboutProfile() { }

        public static AboutProfile Create(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("Tên không được rỗng.");

            return new AboutProfile { FullName = fullName.Trim() };
        }

        public void Update(
            string fullName,
            string? tagline,
            string? bio,
            string? location,
            string? contactEmail)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("Tên không được rỗng.");

            FullName = fullName.Trim();
            Tagline = tagline?.Trim();
            Bio = bio?.Trim();
            Location = location?.Trim();
            ContactEmail = contactEmail is not null ? Email.Create(contactEmail) : null;
            SetUpdated();
        }

        public void SetAvatar(Guid? mediaId)
        {
            AvatarMediaId = mediaId;
            SetUpdated();
        }

        public void SetCv(Guid? mediaId)
        {
            CvMediaId = mediaId;
            SetUpdated();
        }
    }


}

