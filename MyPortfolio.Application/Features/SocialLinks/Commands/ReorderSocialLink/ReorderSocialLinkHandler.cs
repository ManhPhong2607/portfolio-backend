using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.SocialLinks.Commands.ReorderSocialLink
{
    public class ReorderSocialLinkHandler : IRequestHandler<ReorderSocialLinkCommand>
    {
        private readonly ISocialLinkRepository _socialLinkRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ReorderSocialLinkHandler(ISocialLinkRepository socialLinkRepository, IUnitOfWork unitOfWork)
        {
            _socialLinkRepository = socialLinkRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ReorderSocialLinkCommand request, CancellationToken ct)
        {
            var links = await _socialLinkRepository.GetAllAsync(ct);

            for (var i = 0; i < request.OrderedIds.Count; i++)
            {
                var link = links.FirstOrDefault(l => l.Id == request.OrderedIds[i])
                    ?? throw new NotFoundException($"SocialLink {request.OrderedIds[i]} không tồn tại.");

                link.SetOrder(i);
                await _socialLinkRepository.UpdateAsync(link, ct);
            }

            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
