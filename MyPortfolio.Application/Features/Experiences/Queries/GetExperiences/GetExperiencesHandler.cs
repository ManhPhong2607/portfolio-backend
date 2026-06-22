using MediatR;
using MyPortfolio.Application.Features.Experiences.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Experiences.Queries.GetExperiences
{
    public class GetExperiencesHandler : IRequestHandler<GetExperiencesQuery, List<ExperienceDto>>
    {
        private readonly IExperienceRepository _experienceRepository;
        public GetExperiencesHandler(IExperienceRepository experienceRepository)
        {
            _experienceRepository = experienceRepository;
        }

        public async Task<List<ExperienceDto>> Handle(GetExperiencesQuery request, CancellationToken ct)
        {
            var experiences = await _experienceRepository.GetAllAsync(ct);
            return experiences.Select(e=> new ExperienceDto(
                Id: e.Id,
                CompanyName: e.CompanyName,
                Position: e.Position,
                Location: e.Location,
                Description: e.Description,
                EmploymentType: e.EmploymentType,
                StartDate: e.StartDate,
                EndDate: e.EndDate,
                IsCurrent: e.IsCurrent,
                DisplayOrder: e.DisplayOrder
                )).ToList();
        }
    }
}
