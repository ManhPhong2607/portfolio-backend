using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.SocialLinks.Commands.UpdateSocialLink
{
    public class UpdateSocialLinkHandler : IRequestHandler<UpdateSocialLinkCommand>
    {
        private readonly ISocialLinkRepository _socialLinkRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSocialLinkHandler(ISocialLinkRepository socialLinkRepository, IUnitOfWork unitOfWork)
        {
            _socialLinkRepository = socialLinkRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateSocialLinkCommand request, CancellationToken ct)
        {
            var link = await _socialLinkRepository.GetByIdAsync(request.Id, ct)
                ?? throw new DomainException($"Socialink{request.Id} không tôn tại");

            link.Update(
                request.Url,
                request.Label,
                request.IconUrl,
                request.IsVisible
                );
            await _socialLinkRepository.UpdateAsync(link, ct);
            await _unitOfWork.SaveChangesAsync(ct);

        }
    }
}
