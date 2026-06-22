using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Application.Features.Experiences.DTOs;

namespace MyPortfolio.Application.Features.Experiences.Queries.GetExperiences
{
    public record GetExperiencesQuery : IRequest<List<ExperienceDto>>;
    
}
