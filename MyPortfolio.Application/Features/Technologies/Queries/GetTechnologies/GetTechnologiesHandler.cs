using MediatR;
using MyPortfolio.Application.Features.Technologies.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Technologies.Queries.GetTechnologies
{
    public class GetTechnologiesHandler (ITechnologyRepository repository)
         : IRequestHandler<GetTechnologiesQuery, List<TechnologyDto>>
    {
        public async Task<List<TechnologyDto>> Handle(
            GetTechnologiesQuery request, CancellationToken ct)
        {
            //var technologies = await repository.GetAllAsync(ct);

            //return technologies.Select(t => new TechnologyDto(
            //    Id: t.Id,
            //    Name: t.Name,
            //    IconUrl: t.IconUrl
            //)).ToList();

            var technologies = await repository.GetAllAsync(ct);
           // var counts = await repository.GetProjectCountsAsync(ct);           

            return technologies.Select(t => new TechnologyDto(
                Id: t.Id,
                Name: t.Name,
                IconUrl: t.IconUrl,
                ProjectCount: t.Projects.Count
               // ProjectCount: counts.GetValueOrDefault(t.Id, 0)
                
            )).ToList();
        }
    }
}
