using MediatR;
using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Experiences.Commands.UpdateExperience
{
    public record UpdateExperienceCommand(
    Guid Id,
    string CompanyName,
    string Position,
    EmploymentType EmploymentType,
    DateTime StartDate,
    DateTime? EndDate,
    bool IsCurrent,
    string? Location,
    string? Description
) : IRequest;
}
