using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Entities;
namespace MyPortfolio.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagHandler(ITagRepository repository, IUnitOfWork unitOfWork)
        : IRequestHandler<CreateTagCommand, Guid>
    {
        public async Task<Guid> Handle(CreateTagCommand request, CancellationToken ct)
        {
           var exist = await repository.GetAllAsync(ct);
            if (exist.Any(t => t.Name.Equals(request.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                throw new DomainException($"Tag '{request.Name}' đã tồn tại");

            var tag = Tag.Create(request.Name);
            await repository.AddAsync(tag);
            await unitOfWork.SaveChangesAsync();
            return tag.Id;
           
        }
    }
}
