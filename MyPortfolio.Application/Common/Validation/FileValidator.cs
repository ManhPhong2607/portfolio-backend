using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyPortfolio.Application.Common.Validation
{
    public static class FileValidator
    {
        private const long MaxImageSizeBytes = 5 * 1024 * 1024; // 5MB
        private const long MaxRawSizeBytes = 10 * 1024 * 1024;  // 10MB pdf

        private static readonly Dictionary<string, byte[]> ImageMagicBytes = new()
        {
            { "jpg", new byte[] { 0xFF, 0xD8, 0xFF } },
            { "png", new byte[] { 0x89, 0x50, 0x4E, 0x47 } },
            { "gif", new byte[] { 0x47, 0x49, 0x46, 0x38 } },
            { "webp", new byte[] { 0x52, 0x49, 0x46, 0x46 } }
        };

        private static readonly byte[] PdfMagicBytes = new byte[] { 0x25, 0x50, 0x44, 0x46 }; // %pdf

        private static readonly HashSet<string> AllowedImageTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/webp"
        };

        private static readonly HashSet<string> AllowedRawTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "application/pdf"
        };

        public static (bool IsValid, string? Error) ValidateImage(Stream stream, string contentType, long sizeBytes)
        {
            if (!AllowedImageTypes.Contains(contentType))
                return (false, $"Định dạng không hỗ trợ. Chỉ chấp nhận: {string.Join(", ", AllowedImageTypes)}");

            if (sizeBytes > MaxImageSizeBytes)
                return (false, $"File vượt quá 5MB. Kích thước hiện tại: {sizeBytes / 1024 / 1024:F1}MB");

            if (!CheckMagicBytes(stream))
                return (false, "File không hợp lệ (nội dung không khớp với định dạng).");

            return (true, null);
        }

        public static (bool IsValid, string? Error) ValidatePdf(Stream stream, string contentType, long sizeBytes)
        {
            if (!AllowedRawTypes.Contains(contentType))
                return (false, "Chỉ chấp nhận file PDF.");

            if (sizeBytes > MaxRawSizeBytes)
                return (false, "File vượt quá 10MB.");

            if (!CheckPdfMagicBytes(stream))
                return (false, "File không phải PDF hợp lệ.");

            return (true, null);
        }

        private static bool CheckPdfMagicBytes(Stream stream)
        {
            var header = new byte[4];
            var read = stream.Read(header, 0, 4);
            stream.Position = 0;
            return read == 4 && header.SequenceEqual(PdfMagicBytes);
        }

        private static bool CheckMagicBytes(Stream stream)
        {
            var header = new byte[8];
            var read = stream.Read(header, 0, header.Length);
            stream.Position = 0;

            if (read < 4)
                return false;

            foreach (var magic in ImageMagicBytes.Values)
            {
                if (header.Take(magic.Length).SequenceEqual(magic))
                    return true;
            }

            return false;
        }
    }
}
