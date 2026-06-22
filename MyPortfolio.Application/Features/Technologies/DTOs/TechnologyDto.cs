using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Technologies.DTOs
{
    public record TechnologyDto(
    Guid Id,
    string Name,
    string? IconUrl,
    int ProjectCount
    );
}
