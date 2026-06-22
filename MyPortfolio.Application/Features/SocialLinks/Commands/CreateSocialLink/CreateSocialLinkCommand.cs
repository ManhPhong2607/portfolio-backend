using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.SocialLinks.Commands.CreateSocialLink
{
    public record CreateSocialLinkCommand(
        string Platform,
        string Url,
        string? Label,       
        string? IconUrl,
        bool IsVisible = true
        ) :IRequest<Guid>;
}
