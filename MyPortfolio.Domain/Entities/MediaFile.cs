using MyPortfolio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Entities
{
    public class MediaFile : BaseEntity
    {
        public string PublicId { get; private set; }    // Cloudinary public_id
        public string SecureUrl { get; private set; }   // Cloudinary URL
        public string ResourceType { get; private set; }
        public string? OriginalFilename { get; private set; }
        public long? SizeBytes { get; private set; }
        public int? Width { get; private set; }
        public int? Height { get; private set; }
        public string? Folder { get; private set; }
        public DateTime UploadedAt { get; private set; }

        private MediaFile() { }

        public static MediaFile Create(string publicId, string secureUrl,
            string resourceType, string? originalFilename = null,
            long? sizeBytes = null, int? width = null,
            int? height = null, string? folder = null)
        {
            if (string.IsNullOrWhiteSpace(publicId))
                throw new DomainException("PublicId không được rỗng.");

            if (string.IsNullOrWhiteSpace(secureUrl))
                throw new DomainException("SecureUrl không được rỗng.");

            if (resourceType != "image" && resourceType != "raw")
                throw new DomainException("ResourceType phải là 'image' hoặc 'raw'.");

            return new MediaFile
            {
                PublicId = publicId,
                SecureUrl = secureUrl,
                ResourceType = resourceType,
                OriginalFilename = originalFilename,
                SizeBytes = sizeBytes,
                Width = width,
                Height = height,
                Folder = folder,
                UploadedAt = DateTime.UtcNow
            };
        }
    }

}

