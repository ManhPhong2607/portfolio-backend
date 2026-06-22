using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.SocialLinks.Commands.DeleteSocialLink
{
    public class DeleteSocialLinkHandler : IRequestHandler<DeleteSocialLinkCommand>
    {
        private readonly ISocialLinkRepository _socialLinkRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSocialLinkHandler( ISocialLinkRepository socialLinkRepository,IUnitOfWork unitOfWork)
        {
            _socialLinkRepository = socialLinkRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteSocialLinkCommand request, CancellationToken ct)
        {
            var link = await _socialLinkRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"SocialLink {request.Id} không tồn tại");

            await _socialLinkRepository.DeleteAsync(link, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
