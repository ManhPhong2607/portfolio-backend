using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyPortfolio.Application.Features.About.DTOs;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
namespace MyPortfolio.Application.Features.About.Queries.GetAboutProfile
{
    public class GetAboutProfileHandler : IRequestHandler<GetAboutProfileQuery, AboutProfileDto>
    {
        private readonly IAboutProfileRepository _aboutProfileRepository;

        public GetAboutProfileHandler(
            IAboutProfileRepository aboutProfileRepository)
        {
            _aboutProfileRepository = aboutProfileRepository;
        }

        //public async Task<AboutProfileDto> Handle(GetAboutProfileQuery request, CancellationToken ct)
        //{
        //    var profile = await _aboutProfileRepository.GetAsync(ct);
        //    if (profile == null)
        //    {
        //        return new AboutProfileDto(
        //            Id: null,
        //            FullName: null,
        //            Tagline: null,
        //            Bio: null,
        //            Location: null,
        //            ContactEmail: null,
        //            AvatarUrl: null,
        //            CvUrl: null
        //            );
        //    }

        //    return new AboutProfileDto(
        //        Id: profile.Id,
        //        FullName: profile.FullName,
        //        Tagline: profile.Tagline,
        //        Bio: profile.Bio,
        //        Location: profile.Location,
        //        ContactEmail: profile.ContactEmail?.Value,
        //        AvatarUrl: profile.Avatar?.SecureUrl,
        //        CvUrl: profile.CvFile?.SecureUrl
        //        );
        //}
        public async Task<AboutProfileDto> Handle(
            GetAboutProfileQuery request, CancellationToken ct)
        {
            var profile = await _aboutProfileRepository.GetAsync(ct);

            return new AboutProfileDto(
                Id: profile?.Id,
                FullName: profile?.FullName,
                Tagline: profile?.Tagline,
                Bio: profile?.Bio,
                Location: profile?.Location,
                ContactEmail: profile?.ContactEmail?.Value,
                AvatarUrl: profile?.Avatar?.SecureUrl,
                CvUrl: profile?.CvFile?.SecureUrl
            );
        }

    }

}
