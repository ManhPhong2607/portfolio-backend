using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Experiences.DTOs
{
    public record ExperienceDto
    ( Guid Id,
        string CompanyName,
        string Position,
        string? Location,
        string? Description,
        EmploymentType EmploymentType,
        DateTime StartDate,
        DateTime? EndDate,
        bool IsCurrent,
        int DisplayOrder
    );
}
