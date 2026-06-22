using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace MyPortfolio.Infrastructure.Persistence.Repositories
{
    public class AboutProfileRepository : IAboutProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public AboutProfileRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<AboutProfile?> GetAsync(CancellationToken ct = default)
        {
            return await _context.AboutProfiles
                .Include(x=> x.Avatar)
                .Include(x=> x.CvFile)
                .FirstOrDefaultAsync(ct);
        }

        public Task UpdateAsync(AboutProfile Profile, CancellationToken ct)
        {
             _context.AboutProfiles.Update(Profile);
             return Task.CompletedTask;
        }

    }
}
