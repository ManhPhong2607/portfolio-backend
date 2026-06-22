using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Common
{
    public record PaginatedResult<T>(
           List<T> Items,
           int TotalCount,
           int Page,
           int Limit
       )
    {
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / Limit);
        public bool HashNext => Page < TotalPages;
        public bool HasPrev => Page > 1;
    }

}
