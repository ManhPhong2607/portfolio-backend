using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.About.DTOs
{
    public record AboutProfileDto
    (
        Guid? Id,
        string? FullName,
        string? Tagline,
        string? Bio,
        string? Location ,
        string? ContactEmail,
        string? AvatarUrl, // Avtar.SecuredUrl
        string? CvUrl // CvFile.SecuredUrl

    );
}
