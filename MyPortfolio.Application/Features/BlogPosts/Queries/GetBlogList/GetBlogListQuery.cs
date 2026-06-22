using MediatR;
using MyPortfolio.Application.Features.BlogPosts.DTOs;
using MyPortfolio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Queries.GetBlogList
{
    public record GetBlogListQuery(int Page = 1,
        int Limit = 10, 
        string? TagSlug = null,
        string? Search = null
        ) : IRequest<PaginatedResult<BlogPostSummaryDto>>;

}
