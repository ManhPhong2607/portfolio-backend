using MediatR;
using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Skills.Commands.UpdateSkill
{
    public record UpdateSkillCommand(
        Guid Id,
        string Name,
        SkillCategory Category,
        int ProficiencyLevel,
        string? IconUrl) : IRequest;
}
