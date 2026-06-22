using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.Commands.UpdateProject
{
    public record UpdateProjectCommand(
        Guid Id,
        string Title,
        string? ShortDescription,
        string? DetailContent,
        string? DemoUrl,
        string? GithubUrl,
        DateTime? StartDate,
        DateTime? EndDate,
        Guid? ThumbnailMediaId,
        List<Guid> TechnologyIds
        ): IRequest;

}
