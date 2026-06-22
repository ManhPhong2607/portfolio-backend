using Microsoft.EntityFrameworkCore;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Infrastructure.Persistence.Repositories
{
    public class MediaFileRepository : IMediaFileRepository
    {
        private readonly ApplicationDbContext _context;

        public MediaFileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MediaFile file, CancellationToken ct = default)
        {
            await _context.MediaFiles.AddAsync(file, ct);
        }

        public Task DeleteAsync(MediaFile file, CancellationToken ct = default)
        {
            _context.MediaFiles.Remove(file);
            return Task.CompletedTask;
        }

        public async Task<List<MediaFile>> GetAllAsync(string? folder = null, CancellationToken ct = default)
        {
            var query = _context.MediaFiles.AsQueryable();
            if(!string.IsNullOrWhiteSpace(folder))
                query = query.Where(m=>m.Folder == folder);
            return await query
                .OrderByDescending(m => m.UploadedAt)
                .ToListAsync();
        }

        public async Task<MediaFile?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.MediaFiles.FirstOrDefaultAsync(m=>m.Id == id, ct);
        }

        public async Task<MediaFile?> GetByPublicIdAsync(string publicId, CancellationToken ct = default)
        {
            return await _context.MediaFiles.FirstOrDefaultAsync(m=> m.PublicId == publicId, ct); 
        }

        public async Task<bool> IsUsedAsync(Guid id, CancellationToken ct = default)
        {
            //kiểm tra fk ở tất cả table dùng ảnh
            var usedInBlogPosts = await _context.BlogPosts.AnyAsync(b=>b.CoverMediaId == id, ct);
            if(usedInBlogPosts) return true;

            var usedInProjects = await _context.Projects.AnyAsync(p=>p.ThumbnailMediaId == id, ct); 
            if(usedInProjects) return true;

            var usedInProfile = await _context.AboutProfiles.AnyAsync(a => a.AvatarMediaId == id || a.CvMediaId == id, ct);
            return usedInProfile;
        }

    }
}
