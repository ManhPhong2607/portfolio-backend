using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Experiences.Commands.ReorderExperience
{
    public record ReorderExperienceCommand(List<Guid> OrderIds) : IRequest;

}
