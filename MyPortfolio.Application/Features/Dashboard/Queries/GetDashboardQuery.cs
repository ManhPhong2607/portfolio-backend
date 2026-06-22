using MediatR;
using MyPortfolio.Application.Features.Dashboard.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Dashboard.Queries
{
    public record GetDashboardQuery : IRequest<DashboardDto>;
}
