using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.DTOs
{
    public record BlogTagDto(
       Guid Id,
       string Name,
       string Slug
   );
}
