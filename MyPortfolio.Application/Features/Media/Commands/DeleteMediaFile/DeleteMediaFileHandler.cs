using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.Media.Commands.DeleteMediaFile
{
    public class DeleteMediaFileHandler : IRequestHandler<DeleteMediaFileCommand, DeleteMediaFileResult>
    {
        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly IStorageService _storageService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMediaFileHandler(IMediaFileRepository mediaFileRepository, IStorageService storageService, IUnitOfWork unitOfWork)
        {
            _mediaFileRepository = mediaFileRepository;
            _storageService = storageService;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteMediaFileResult> Handle(DeleteMediaFileCommand request, CancellationToken ct)
        {
            var mediaFile = await _mediaFileRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"MediaFile {request.Id} không tồn tại.");

            //kiem tra dang dung o dau
            var isUsed = await _mediaFileRepository.IsUsedAsync(request.Id, ct);

            if (isUsed && !request.ForceDelete)
            {
                return new DeleteMediaFileResult(
                    Deleted: false,
                    WasInUse: true,
                    Message: "Ảnh đang được sử dụng. Dùng Force-delete = true để xóa bắt buộc.");
            }

            //xóa trên cloudinary
            await _storageService.DeleteAsync(mediaFile.PublicId, mediaFile.ResourceType);

            //xóa record db
            // Nếu ForceDelete và đang dùng → EF sẽ SET NULL các FK (đã cấu hình OnDelete: SetNull)
            await _mediaFileRepository.DeleteAsync(mediaFile, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return new DeleteMediaFileResult(
                Deleted: true,
                WasInUse: isUsed,
                Message: isUsed ? "Đã xóa. Các liên kết đến ảnh đã được gỡ bỏ." : "Đã xóa thành công." 
                );

        }
    }
}
