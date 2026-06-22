using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Commands.DeleteBlog
{
    public class DeleteBlogPostHandler(
         IBlogPostRepository blogRepository,
         IStorageService storageService,
         IMediaFileRepository mediaFileRepository,
         IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteBlogPostCommand>
    {
        public async Task Handle(DeleteBlogPostCommand request, CancellationToken ct)
        {
            var post = await blogRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Bài viết {request.Id} không tồn tại.");

            await blogRepository.DeleteAsync(post, ct);
            await unitOfWork.SaveChangesAsync(ct);
            // Cover image SET NULL tự động 
            // Ảnh vẫn tồn tại trong MediaFiles — Admin xoá thủ công qua Media Library
        }
    }
}
