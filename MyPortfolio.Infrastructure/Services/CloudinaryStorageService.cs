using MyPortfolio.Domain.Interfaces.Services;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
namespace MyPortfolio.Infrastructure.Services
{
    public class CloudinaryStorageService : IStorageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryStorageService(IConfiguration configuration)
        {
            var cloudName = configuration["Cloudinary:CloudName"]
                ?? throw new InvalidOperationException("Cloudinary: CloudName chưa cấu hình.");

            var apiKey = configuration["Cloudinary:ApiKey"]
                ?? throw new InvalidOperationException("Cloudinary: ApiKey chưa cấu hình.");

            var apiSecret = configuration["Cloudinary:ApiSecret"]
                ?? throw new InvalidOperationException("Cloudinary: ApiSecret chưa cấu hình.");

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account) { Api = { Secure = true } };
        }

        public async Task DeleteAsync(string publicId, string resourceType = "image")
        {
            var resType = resourceType == "raw"
                ? CloudinaryDotNet.Actions.ResourceType.Raw
                : CloudinaryDotNet.Actions.ResourceType.Image;

            var deleteParams = new DeletionParams(publicId)
            {
                ResourceType = resType
            };

            var result = await _cloudinary.DestroyAsync(deleteParams);

            // "ok"      : xóa thành công
            // "not found": file đã không tồn tại trên Cloudinary
            //              (có thể đã bị xóa thủ công hoặc xóa trước đó)
            //              => vẫn coi là thành công.
            if (result.Result == "ok" || result.Result == "not found")
            {
                return;
            }

            throw new InvalidOperationException(
                $"Cloudinary xoá thất bại: {result.Result}"
            );
        }

        //public async Task DeleteAsync(string publicId, string resourceType = "image")
        //{
        //    var resType = resourceType == "raw"
        //        ? CloudinaryDotNet.Actions.ResourceType.Raw
        //        : CloudinaryDotNet.Actions.ResourceType.Image;

        //    var deleteParams = new DeletionParams(publicId) { ResourceType = resType };
        //    var result = await _cloudinary.DestroyAsync(deleteParams);

        //    if (result.Result != "ok")
        //        throw new InvalidOperationException($"Cloudinary xoá thất bại: {result.Result}");
        //}

        public async Task<FileUploadResult> UploadAsync(
            Stream fileStream,
            string fileName,
            string folder,
            string resourceType = "image",
            CancellationToken ct = default)
        {
            if (resourceType == "image")
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, fileStream),
                    Folder = $"portfolio/{folder}",
                    Transformation = new Transformation()
                       .Quality("auto")
                       .FetchFormat("auto"),   //convert sang webp
                    UseFilename = true,
                    UniqueFilename = true
                };
                var result = await _cloudinary.UploadAsync(uploadParams);
                if (result.Error != null)
                    throw new InvalidOperationException($"Cloudinary lỗi: {result.Error.Message}");

                return new FileUploadResult(
                    PublicId: result.PublicId,
                    SecureUrl: result.SecureUrl.ToString(),
                    ResourceType: "image",
                    Width: result.Width,
                    Height: result.Height,
                    SizeBytes: result.Bytes
                    );
            }
            else // raw (PDF, doc...)
            {
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(fileName, fileStream),
                    Folder = $"portfolio/{folder}",
                    UseFilename = true,
                    UniqueFilename = true
                };

                var result = await _cloudinary.UploadAsync(uploadParams);

                if (result.Error is not null)
                    throw new InvalidOperationException($"Cloudinary lỗi: {result.Error.Message}");

                return new FileUploadResult(
                    PublicId: result.PublicId,
                    SecureUrl: result.SecureUrl.ToString(),
                    ResourceType: "raw",
                    Width: null,
                    Height: null,
                    SizeBytes: result.Bytes
                );

            }
        }
    }
}
