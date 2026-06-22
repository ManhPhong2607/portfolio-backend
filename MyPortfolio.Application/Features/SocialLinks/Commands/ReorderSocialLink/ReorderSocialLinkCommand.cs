using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.SocialLinks.Commands.ReorderSocialLink
{
    public record ReorderSocialLinkCommand(List<Guid> OrderedIds) :IRequest;

}
