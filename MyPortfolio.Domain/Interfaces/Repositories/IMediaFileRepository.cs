using MyPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Interfaces.Repositories
{
    public interface IMediaFileRepository
    {
        Task<List<MediaFile>> GetAllAsync(string? folder = null, CancellationToken ct = default);
        Task<MediaFile?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<MediaFile?> GetByPublicIdAsync(string publicId, CancellationToken ct = default);
        Task<bool> IsUsedAsync(Guid id, CancellationToken ct = default); //kiểm tra mật khẩu để tránh việc ảnh đang sử dụng bị xóa đột ngột mà không có thông báo
        Task AddAsync(MediaFile file, CancellationToken ct = default);
        Task DeleteAsync(MediaFile file, CancellationToken ct = default);
    }
}
