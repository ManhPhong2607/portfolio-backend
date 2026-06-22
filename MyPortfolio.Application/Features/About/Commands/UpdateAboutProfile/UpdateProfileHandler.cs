using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;

namespace MyPortfolio.Application.Features.About.Commands.UpdateAboutProfile
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IAboutProfileRepository _aboutProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProfileHandler(
            IAboutProfileRepository aboutProfileRepository,
            IUnitOfWork unitOfWork)
        {
            _aboutProfileRepository = aboutProfileRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateProfileCommand request, CancellationToken ct)
        {
            var profile = await _aboutProfileRepository.GetAsync(ct)
                ?? throw new DomainException("Chưa có thông tin profile.");

            profile.Update(
                fullName: request.FullName,
                tagline: request.Tagline,
                bio: request.Bio,
                location: request.Location,
                contactEmail: request.ContactEmail);

            //profile.SetAvatar(request.AvatarMediaId);
            //profile.SetCv(request.CvMediaId);

            //if (request.AvatarMediaId.HasValue)
            //    profile.SetAvatar(request.AvatarMediaId.Value);

            //if (request.CvMediaId.HasValue)
            //    profile.SetCv(request.CvMediaId.Value);

            if (request.AvatarMediaId.HasValue)
            {
                var avatarValue = request.AvatarMediaId.Value == Guid.Empty
                    ? (Guid?)null
                    : request.AvatarMediaId.Value;
                profile.SetAvatar(avatarValue);
            }

            if (request.CvMediaId.HasValue)
            {
                var cvValue = request.CvMediaId.Value == Guid.Empty
                    ? (Guid?)null
                    : request.CvMediaId.Value;
                profile.SetCv(cvValue);
            }

            await _aboutProfileRepository.UpdateAsync(profile, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }


}
