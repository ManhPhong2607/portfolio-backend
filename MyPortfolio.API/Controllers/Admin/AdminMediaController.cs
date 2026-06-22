using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Features.Media.Commands.DeleteMediaFile;
using MyPortfolio.Application.Features.Media.Commands.UploadMediaFile;
using MyPortfolio.Application.Features.Media.Queries.GetMediaFiles;

namespace MyPortfolio.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/media")]
    [Authorize]
    public class AdminMediaController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? Folder, CancellationToken ct)
        {
            var result = await mediator.Send(new GetMediaFilesQuery(Folder), ct);
            return Ok(result);
        }

        [HttpPost("upload")]
        [RequestSizeLimit(10 * 1024 * 1024)] // 10MB limit
        public async Task<IActionResult> Upload(
        IFormFile file,
        [FromQuery] string folder = "general",
        CancellationToken ct = default)
        {
            if (file is null || file.Length == 0)
                return BadRequest(new { error = "Không có file được gửi lên." });

            using var stream = file.OpenReadStream();

            var command = new UploadMediaFileCommand(
                FileStream: stream,
                FileName: file.FileName,
                ContentType: file.ContentType,
                SizeBytes: file.Length,
                Folder: folder
            );

            var result = await mediator.Send(command, ct);
            return CreatedAtAction(nameof(Upload), result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
        Guid id,
        [FromQuery] bool force = false,
        CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new DeleteMediaFileCommand(id, force), ct);

            if (!result.Deleted)
                return Conflict(new { result.WasInUse, result.Message });

            return Ok(new { result.Deleted, result.WasInUse, result.Message });
        }
    }
}
