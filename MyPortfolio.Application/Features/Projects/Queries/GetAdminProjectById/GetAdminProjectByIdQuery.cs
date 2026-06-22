using MediatR;
using MyPortfolio.Application.Features.Projects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.Queries.GetAdminProjectById
{
   public record GetAdminProjectByIdQuery(Guid Id) : IRequest<ProjectDetailDto>;
}
