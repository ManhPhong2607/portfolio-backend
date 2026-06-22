using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace MyPortfolio.Application.Features.Experiences.Commands.DeleteExperience
{
    public record DeleteExperienceCommand(Guid Id) : IRequest;

}
