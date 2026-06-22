using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace MyPortfolio.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            await _context.Users.AddAsync(user, ct);       
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
          return await _context.Users.FirstOrDefaultAsync(u => u.Email.Value == email.ToLowerInvariant(), ct);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
        }

        public async Task UpdateAsync(User user, CancellationToken ct = default)
        {
            _context.Users.Update(user);         
        }
                
    }
}
