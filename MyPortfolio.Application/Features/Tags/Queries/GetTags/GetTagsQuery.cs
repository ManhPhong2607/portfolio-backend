using MediatR;
using MyPortfolio.Application.Features.Tags.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Tags.Queries.GetTags
{
    public record GetTagsQuery : IRequest<List<TagDto>>;

}
