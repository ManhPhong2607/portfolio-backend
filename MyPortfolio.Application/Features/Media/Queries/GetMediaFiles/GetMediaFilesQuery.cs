using MediatR;
using MyPortfolio.Application.Features.Media.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Media.Queries.GetMediaFiles
{
    public record GetMediaFilesQuery(string? Folder = null) : IRequest<List<MediaFileDto>>;

}
