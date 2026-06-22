using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace MyPortfolio.Application.Features.Experiences.Commands.CreateExperience
{
    public record CreateExperienceCommand(
        string CompanyName,
        string Position,
        EmploymentType EmploymentType,
        DateTime StartDate,
        DateTime? EndDate,
        bool IsCurrent,
        string? Location,
        string? Description) : IRequest<Guid>;
    
}
