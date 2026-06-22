using MyPortfolio.Application.Features.Technologies.DTOs;
using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.DTOs
{
    public record ProjectDetailDto(
    Guid Id,
    string Title,
    string Slug,
    string? ShortDescription,
    string? DetailContent,
    string? ThumbnailUrl,
    string? DemoUrl,
    string? GithubUrl,
    ProjectStatus Status,
    int DisplayOrder,
    bool IsFeatured,
    DateTime? StartDate,
    DateTime? EndDate,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<ProjectTechnologyDto> Technologies
    );
}
