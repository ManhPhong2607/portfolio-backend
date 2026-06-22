using MediatR;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.SocialLinks.Commands.CreateSocialLink
{
    public class CreateSocialLinkHandler :IRequestHandler<CreateSocialLinkCommand, Guid>
    {
        private readonly ISocialLinkRepository _socialLinkRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSocialLinkHandler(ISocialLinkRepository socialLinkRepository, IUnitOfWork unitOfWork)
        {
            _socialLinkRepository = socialLinkRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateSocialLinkCommand request, CancellationToken ct)
        {
            var link = SocialLink.Create(
                request.Platform,
                request.Url,
                request.Label,
                request.IconUrl
                //request.IsVisible
                );

            if (!request.IsVisible)
                link.ToggleVisibility();

            //if( request.IconUrl != null)
            //{
            //    link.Update(
            //        request.Url,
            //        request.Label,
            //        request.IconUrl);
            //}
            await _socialLinkRepository.AddAsync(link, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return link.Id;
        }
    }
}
