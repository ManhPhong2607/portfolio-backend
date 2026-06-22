using MediatR;
using MyPortfolio.Application.Features.Media.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Media.Commands.UploadMediaFile
{
    public record UploadMediaFileCommand(
        Stream FileStream,
        string FileName,
        string ContentType,
        long SizeBytes,
        string Folder    // "blog" | "projects" | "avatar" | "cv" | "general"
        ) :IRequest<MediaFileDto>;  
}
