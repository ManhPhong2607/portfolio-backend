using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MyPortfolio.Domain.Interfaces.Services;
namespace MyPortfolio.Infrastructure.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private ClaimsPrincipal? User
            => httpContextAccessor.HttpContext?.User;

        public Guid UserId
        {
            get
            {
                var sub = User?.FindFirstValue(ClaimTypes.NameIdentifier)
                       ?? User?.FindFirstValue("sub");
                return Guid.TryParse(sub, out var id) ? id : Guid.Empty;
            }
        }

        public string Role
            => User?.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

        public bool IsAuthenticated
            => User?.Identity?.IsAuthenticated ?? false;
    }
}
