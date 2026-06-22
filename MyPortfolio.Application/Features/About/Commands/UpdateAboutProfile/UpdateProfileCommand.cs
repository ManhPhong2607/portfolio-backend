using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.About.Commands.UpdateAboutProfile
{
    public record UpdateProfileCommand(
        string FullName,
        string? Tagline,
        string? Bio,
        string? Location,
        string? ContactEmail,
        Guid? AvatarMediaId,
        Guid? CvMediaId
        ) : IRequest;

}
