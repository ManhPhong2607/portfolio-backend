using MediatR;
using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Commands.ChangeBlogStatus
{
    public record ChangeBlogStatusCommand(
        Guid Id,
        PostStatus Status
        ) : IRequest;
}
