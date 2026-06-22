using MediatR;
using MyPortfolio.Application.Features.SocialLinks.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.SocialLinks.Queries.GetSocialLinks
{
    public record GetSocialLinksQuery : IRequest<List<SocialLinkDto>>;

}
