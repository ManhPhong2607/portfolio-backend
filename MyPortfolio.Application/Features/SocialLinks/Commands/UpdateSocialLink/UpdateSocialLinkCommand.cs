using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.SocialLinks.Commands.UpdateSocialLink
{
    public record UpdateSocialLinkCommand(
        Guid Id,
        string Url,
        string? Label,
        string? IconUrl,
        bool IsVisible
        ):IRequest;
}
