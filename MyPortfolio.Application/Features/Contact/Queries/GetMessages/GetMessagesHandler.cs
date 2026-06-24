using MediatR;
using MyPortfolio.Application.Features.Contact.DTOs;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Contact.Queries.GetMessages
{
    public class GetMessagesHandler(
       IContactMessageRepository repository
    ) : IRequestHandler<GetMessagesQuery, PaginatedResult<ContactMessageDto>>
    {
        public async Task<PaginatedResult<ContactMessageDto>> Handle(
            GetMessagesQuery request, CancellationToken ct)
        {
            var result = await repository.GetAllAsync(request.Page, request.Limit, request.Status, ct);

            var dtos = result.Items.Select(m => new ContactMessageDto(
                Id: m.Id,
                SenderName: m.SenderName,
                SenderEmail: m.SenderEmail.Value,
                Subject: m.Subject,
                Body: m.Body,
                Status: m.Status.ToString(),
                SentAt: m.SentAt,
                ReadAt: m.ReadAt
            )).ToList();

            return new PaginatedResult<ContactMessageDto>(dtos, result.TotalCount, result.Page, result.Limit);
        }
    }
}
