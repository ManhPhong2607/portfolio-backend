using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Commands.CreateBlog
{
    public class CreateBlogHandler(
        IBlogPostRepository blogPostRepository,
        ITagRepository tagRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateBlogCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBlogCommand request, CancellationToken ct)
        {
            //kiem tra slug
            var post = BlogPost.Create(
            authorId: currentUser.UserId,
            title: request.Title,
            content: request.Content,
            excerpt: request.Excerpt,
            coverMediaId: request.CoverMediaId
        );

            var slugExist = await blogPostRepository.SlugExistsAsync(post.Slug.Value, ct: ct);
            if( slugExist )
                throw new DomainException($"Slug '{post.Slug.Value}' đã tồn tại. Hãy đổi tiêu đề.");

            //gán tag
            if (request.TagIds.Count > 0) 
            {
                var tags = await tagRepository.GetAllAsync(ct);   
                var selectedTags = tags.Where(t => request.TagIds.Contains(t.Id)).ToList(); 
                post.SetTags(selectedTags);
            }
            await blogPostRepository.AddAsync(post, ct);
            await unitOfWork.SaveChangesAsync(ct);
            return post.Id;
        }
    }
}
