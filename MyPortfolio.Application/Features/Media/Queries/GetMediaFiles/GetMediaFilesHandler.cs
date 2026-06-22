using MediatR;
using MyPortfolio.Application.Features.Media.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Media.Queries.GetMediaFiles
{
    public class GetMediaFilesHandler : IRequestHandler<GetMediaFilesQuery, List<MediaFileDto>>
    {
        private readonly IMediaFileRepository _mediaFileRepository;

        public GetMediaFilesHandler(IMediaFileRepository mediaFileRepository)
        {
            _mediaFileRepository = mediaFileRepository;
        }

        public async Task<List<MediaFileDto>> Handle(GetMediaFilesQuery request, CancellationToken ct)
        {
            var files = await _mediaFileRepository.GetAllAsync(request.Folder, ct);

            return files.Select(f => new MediaFileDto(
                 Id: f.Id,
                 PublicId: f.PublicId,
                 SecureUrl: f.SecureUrl,
                 ResourceType: f.ResourceType,
                 OriginalFilename: f.OriginalFilename,
                 SizeBytes: f.SizeBytes,
                 Width: f.Width,
                 Height: f.Height,
                 Folder: f.Folder,
                 UploadedAt: f.UploadedAt
                 )).ToList(); 
        }
    }
}
