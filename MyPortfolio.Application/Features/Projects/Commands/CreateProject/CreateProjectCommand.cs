using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyPortfolio.Domain.Enums;
namespace MyPortfolio.Application.Features.Projects.Commands.CreateProject
{
    public record CreateProjectCommand(
        string Title,
        string? ShortDescription,
        string? DetailContent,
        string? DemoUrl,
        string? GithubUrl,
        DateTime? StartDate,
        DateTime? EndDate,
        Guid? ThumbnailMediaId,
        List<Guid> TechnologyIds
        ) : IRequest<Guid>; 
}
