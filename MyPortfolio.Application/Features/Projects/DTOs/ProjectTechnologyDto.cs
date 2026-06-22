using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Projects.DTOs
{
    public record ProjectTechnologyDto
    (
        Guid Id,
        string Name,
        string? IconUrl
    );
}
