using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.SocialLinks.Commands.DeleteSocialLink
{
    public record DeleteSocialLinkCommand(Guid Id): IRequest;
   
}
