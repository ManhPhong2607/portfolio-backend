using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Commands.IncrementViewCount
{
    public class IncrementViewCountHandler(
        IBlogPostRepository blogPostRepository
        ) : IRequestHandler<IncrementViewCountCommand>
    {
        public async Task Handle(IncrementViewCountCommand request, CancellationToken ct)
        {
            var post = await blogPostRepository.GetBySlugAsync(request.Slug, ct);
            if( post == null ) 
                return;
            await blogPostRepository.IncrementViewCountAsync(post.Id, ct);
        }
    }
}
