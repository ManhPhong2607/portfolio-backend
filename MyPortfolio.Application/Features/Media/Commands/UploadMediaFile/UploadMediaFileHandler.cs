using MediatR;
using MyPortfolio.Application.Features.Media.DTOs;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Application.Common.Validation;
using MyPortfolio.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Media.Commands.UploadMediaFile
{
    public class UploadMediaFileHandler : IRequestHandler<UploadMediaFileCommand, MediaFileDto>
    {
        private readonly IStorageService _storageService;
        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UploadMediaFileHandler(
            IStorageService storageService,
            IMediaFileRepository mediaFileRepository,
            IUnitOfWork unitOfWork)
        {
            _storageService = storageService;
            _mediaFileRepository = mediaFileRepository;
            _unitOfWork = unitOfWork;
        }
 
        public async Task<MediaFileDto> Handle(UploadMediaFileCommand request, CancellationToken ct)
        {
            var extension = Path.GetExtension(request.FileName)?.ToLower();
          //  var isRaw = request.ContentType == "application/pdf" || extension == ".pdf" || request.Folder == "cv";
            var isRaw =
                request.Folder == "cv" ||
                request.ContentType == "application/pdf";

            var resourceType = isRaw ? "raw" : "image";
            //string resourceType = "image";
            //if (!isRaw && !request.ContentType.StartsWith("image/"))
            //{
            //    resourceType = "raw";
            //}
            var (isValid, error) = isRaw
                ? FileValidator.ValidatePdf(request.FileStream, request.ContentType, request.SizeBytes)
                : FileValidator.ValidateImage(request.FileStream, request.ContentType, request.SizeBytes);

            if (!isValid)
                throw new DomainException(error!);

            // Upload lên Cloudinary
            var uploadResult = await _storageService.UploadAsync(
                request.FileStream,
                request.FileName,
                request.Folder,
                resourceType,
                ct
            );

            // Lưu metadata vào DB
            var mediaFile = MediaFile.Create(
                publicId: uploadResult.PublicId,
                secureUrl: uploadResult.SecureUrl,
                resourceType: uploadResult.ResourceType,
                originalFilename: request.FileName,
                sizeBytes: uploadResult.SizeBytes,
                width: uploadResult.Width,
                height: uploadResult.Height,
                folder: request.Folder
            );

            await _mediaFileRepository.AddAsync(mediaFile, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return new MediaFileDto(
                Id: mediaFile.Id,
                PublicId: mediaFile.PublicId,
                SecureUrl: mediaFile.SecureUrl,
                ResourceType: mediaFile.ResourceType,
                OriginalFilename: mediaFile.OriginalFilename,
                SizeBytes: mediaFile.SizeBytes,
                Width: mediaFile.Width,
                Height: mediaFile.Height,
                Folder: mediaFile.Folder,
                UploadedAt: mediaFile.UploadedAt
            );
        }
    }
}
