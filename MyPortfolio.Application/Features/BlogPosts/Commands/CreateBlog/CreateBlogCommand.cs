using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Commands.CreateBlog
{
    public record CreateBlogCommand(
        string Title,
        string Content,
        string? Excerpt, 
        Guid? CoverMediaId,
        List<Guid> TagIds
        ) : IRequest<Guid>;
   
}
