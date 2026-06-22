using MediatR;
using MyPortfolio.Application.Features.Projects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.Queries.GetProjectBySlug
{
    public record GetProjectBySlugQuery(string Slug) : IRequest<ProjectDetailDto>;
}
