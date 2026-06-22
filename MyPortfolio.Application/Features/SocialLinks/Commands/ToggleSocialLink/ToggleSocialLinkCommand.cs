using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.SocialLinks.Commands.ToogleSocialLink
{
    public record ToggleSocialLinkCommand(Guid Id): IRequest<bool>;
    
}
