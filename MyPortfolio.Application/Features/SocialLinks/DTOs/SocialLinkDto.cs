using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.SocialLinks.DTOs
{
    public record SocialLinkDto(
        Guid Id,
        string Platform,
        string? Label,
        string Url,
        string? IconUrl,
        int DisplayOrder,
        bool IsVisible
        );
    
}
