using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Commands.DeleteBlog
{
    public record DeleteBlogPostCommand(Guid Id) : IRequest;
}
