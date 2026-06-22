using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.BlogPosts.Commands.UpdateBlog
{
    public class UpdateBlogHandler(
        IBlogPostRepository blogPostRepository,
        ITagRepository tagRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateBlogCommand>
    {
        public async Task Handle(UpdateBlogCommand request, CancellationToken ct)
        {
            var post = await blogPostRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Bài viết {request.Id} không tồn tại.");

            post.Update(request.Title, request.Content, request.Excerpt, request.CoverMediaId);

            var slugExist = await blogPostRepository.SlugExistsAsync(
                post.Slug.Value, excludeId: request.Id, ct: ct );
            if(slugExist)
                throw new DomainException($"Slug {post.Slug.Value} đã tồn tại.");

            //var existing = await blogPostRepository
            //   .GetBySlugAsync(post.Slug.Value, ct);

            //if (existing is not null &&
            //    existing.Id != request.Id)
            //{
            //    throw new DomainException("Slug đã tồn tại.");
            //}

            var tags = await tagRepository.GetAllAsync(ct);
            var selectedTag = tags.Where(t=> request.TagIds.Contains(t.Id)).ToList();
            post.SetTags(selectedTag);
            
            await blogPostRepository.UpdateAsync(post, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
