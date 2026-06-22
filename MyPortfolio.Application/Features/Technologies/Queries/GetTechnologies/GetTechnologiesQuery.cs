using MediatR;
using MyPortfolio.Application.Features.Technologies.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Technologies.Queries.GetTechnologies
{
    public record GetTechnologiesQuery : IRequest<List<TechnologyDto>>;
}
