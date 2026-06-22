using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Application.Features.SocialLinks.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;
namespace MyPortfolio.Application.Features.SocialLinks.Queries.GetAllSocialLinks
{
    public class GetAllSocialLinksHandler: IRequestHandler<GetAllSocialLinksQuery, List<SocialLinkDto>>
    {
        private readonly ISocialLinkRepository _socialLinkRepository;

        public GetAllSocialLinksHandler(ISocialLinkRepository socialLinkRepository)
        {
            _socialLinkRepository = socialLinkRepository;
        }

        public async Task<List<SocialLinkDto>> Handle(GetAllSocialLinksQuery request, CancellationToken ct)
        {
            var links = await _socialLinkRepository.GetAllAsync(ct);
            return links.Select(sl => new SocialLinkDto
            (
                Id : sl.Id,
                Platform : sl.Platform,
                Label : sl.Label,
                Url : sl.Url,
                IconUrl : sl.IconUrl,
                DisplayOrder:  sl.DisplayOrder,
                IsVisible: sl.IsVisible
            )).ToList();
        }
    }
}
