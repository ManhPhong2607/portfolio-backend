using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Media.DTOs
{
    public record MediaFileDto(
        Guid Id,
        string PublicId,
        string SecureUrl,
        string ResourceType,
        string? OriginalFilename,
        long? SizeBytes,
        int? Width,
        int? Height,
        string? Folder,
        DateTime UploadedAt);
}
