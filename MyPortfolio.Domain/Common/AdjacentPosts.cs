using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Common
{
    public record AdjacentPosts(
       Guid? PrevId, string? PrevTitle, string? PrevSlug,
       Guid? NextId, string? NextTitle, string? NextSlug
   );

}
