using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyPortfolio.Application.Features.About.DTOs;
namespace MyPortfolio.Application.Features.About.Queries.GetAboutProfile
{
    public record GetAboutProfileQuery : IRequest<AboutProfileDto>;

}
