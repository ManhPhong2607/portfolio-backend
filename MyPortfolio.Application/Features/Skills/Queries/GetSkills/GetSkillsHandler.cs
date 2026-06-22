using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Application.Features.Skills.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;

namespace MyPortfolio.Application.Features.Skills.Queries.GetSkills
{
    public class GetSkillsHandler : IRequestHandler<GetSkillsQuery, List<SkillDto>>
    {
        private readonly ISkillRepository _skillRepository;

        public GetSkillsHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<List<SkillDto>> Handle(GetSkillsQuery request, CancellationToken ct)
        {
            var skills = await _skillRepository.GetAllAsync(ct);

            return skills.Select(s => new SkillDto(
                s.Id,
                s.Name,
                s.Category,
                s.ProficiencyLevel,
                s.IconUrl,
                s.DisplayOrder
            )).ToList();
        }
    }
}
