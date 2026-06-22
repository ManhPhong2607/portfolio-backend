using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Interfaces.Services
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(Guid userId, string role);
        string GenerateRefreshToken();
        Guid? GetUserIdFromToken(string token);
    }
}
