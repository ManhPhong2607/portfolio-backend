using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Enums;
namespace MyPortfolio.Application.Features.BlogPosts.Commands.ChangeBlogStatus
{
    public class ChangeBlogStatusHandler(
        IBlogPostRepository blogPostRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<ChangeBlogStatusCommand>
    {
        public async Task Handle(ChangeBlogStatusCommand request, CancellationToken ct)
        {
            var post = await blogPostRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"bài viết {request.Id} không tồn tại");

            switch (request.Status)
            {
                case PostStatus.Published:
                    post.Publish();
                    break;

                case PostStatus.Draft:
                    post.Unpublish(); 
                    break;

                case PostStatus.Archived:
                    post.Archive();
                    break;

                default:
                    throw new DomainException("Trạng thái không hợp lệ");
            }
            await blogPostRepository.UpdateAsync( post, ct );
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
