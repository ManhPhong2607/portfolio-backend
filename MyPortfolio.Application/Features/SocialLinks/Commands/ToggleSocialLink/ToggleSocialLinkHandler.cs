using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.SocialLinks.Commands.ToogleSocialLink
{
    public class ToggleSocialLinkHandler : IRequestHandler<ToggleSocialLinkCommand, bool>
    {
        private readonly ISocialLinkRepository _socialLinkRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ToggleSocialLinkHandler(ISocialLinkRepository socialLinkRepository, IUnitOfWork unitOfWork)
        {
            _socialLinkRepository = socialLinkRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ToggleSocialLinkCommand request, CancellationToken ct)
        {
            var link = await _socialLinkRepository.GetByIdAsync(request.Id, ct)
                ?? throw new DomainException($"Socialink{request.Id} không tôn tại");

            link.ToggleVisibility();
            await _socialLinkRepository.UpdateAsync(link, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return link.IsVisible;
        }
    }
}
