using MediatR;
using MyPortfolio.Application.Features.Contact.DTOs;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Queries.GetMessages
{
    public record GetMessagesQuery(
      int Page = 1,
      int Limit = 20,
      MessageStatus? Status = null
    ) : IRequest<PaginatedResult<ContactMessageDto>>;
}
