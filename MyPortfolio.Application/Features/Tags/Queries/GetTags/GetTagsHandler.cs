using MediatR;
using MyPortfolio.Application.Features.Tags.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Tags.Queries.GetTags
{
    public class GetTagsHandler(ITagRepository _repository) : IRequestHandler<GetTagsQuery, List<TagDto>>
    {
        public async Task<List<TagDto>> Handle(GetTagsQuery request, CancellationToken ct)
        {
            var tags = await _repository.GetAllAsync(ct);
            var counts = await _repository.GetBlogCountsAsync(ct);
            return tags.Select(t => new TagDto(
                Id: t.Id,
                Name: t.Name,
                Slug: t.Slug.Value,
                BlogCount: counts.GetValueOrDefault(t.Id, 0)
             )).ToList();
            //var tagsTask = _repository.GetAllAsync(ct);
            //var countsTask = _repository.GetBlogCountsAsync(ct);
            //await Task.WhenAll(tagsTask, countsTask);

            //var tags = tagsTask.Result;
            //var counts = countsTask.Result;
            //return tags.Select(t => new TagDto(
            //    Id: t.Id,
            //    Name: t.Name,
            //    Slug: t.Slug.Value,
            //    BlogCount: counts.GetValueOrDefault(t.Id, 0) 
            //)).ToList();
        }
    }
}
