using MediatR;
using MyPortfolio.Application.Features.BlogPosts.DTOs;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Queries.GetAdminBlogList
{
    public record GetAdminBlogListQuery(int Page = 1,
        int Limit = 10,
        PostStatus? Status = null
    ) : IRequest<PaginatedResult<BlogPostSummaryDto>>;
}
