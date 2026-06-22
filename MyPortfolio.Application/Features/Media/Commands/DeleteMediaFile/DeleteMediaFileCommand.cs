using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Media.Commands.DeleteMediaFile
{
    public record DeleteMediaFileCommand(
        Guid Id,
        bool ForceDelete = false // true = xóa dù đang dùng
        ) : IRequest<DeleteMediaFileResult>;
    
    public record DeleteMediaFileResult(
        bool Deleted,
        bool WasInUse,
        string? Message
        );
}
