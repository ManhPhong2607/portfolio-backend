using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Tags.DTOs
{
    public record TagDto(
        Guid Id,
        string Name,
        string Slug,
        int BlogCount);
   
}
