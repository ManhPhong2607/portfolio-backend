using MediatR;
using MyPortfolio.Application.Features.SocialLinks.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.SocialLinks.Queries.GetSocialLinks
{
    public class GetSocialLinksHandler : IRequestHandler<GetSocialLinksQuery, List<SocialLinkDto>>
    {
        private readonly ISocialLinkRepository _socialLinkRepository;
        public GetSocialLinksHandler(ISocialLinkRepository socialLinkRepository)
        {
            _socialLinkRepository = socialLinkRepository;
        }

        public async Task<List<SocialLinkDto>> Handle(GetSocialLinksQuery request, CancellationToken ct)
        {
            var links = await _socialLinkRepository.GetVisibleAsync(ct);
            return links.Select(link => new SocialLinkDto(
                Id: link.Id,
                Platform: link.Platform,
                Label: link.Label,
                Url: link.Url,
                IconUrl: link.IconUrl,
                DisplayOrder: link.DisplayOrder,
                IsVisible: link.IsVisible
            )).ToList();
        }
    }
}
