using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyPortfolio.Application.Features.Skills;
using MyPortfolio.Application.Features.Skills.DTOs;
namespace MyPortfolio.Application.Features.Skills.Queries.GetSkills
{
    public record GetSkillsQuery : IRequest<List<SkillDto>>;
    
}
