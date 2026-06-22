using MyPortfolio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Entities
{
    public class ReFreshToken : BaseEntity
    {
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public bool IsRevoked { get; private set; }

        public User User { get; private set; } = default!;
        private ReFreshToken() { }

        public static ReFreshToken Create(Guid userId, string token, int expiryDays = 7)
        {
            if(string.IsNullOrEmpty(token)) 
                throw new DomainException("Token không được rỗng"); 
            
            return new ReFreshToken()
            {
                UserId = userId,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddDays(expiryDays),
                IsRevoked = false
            };
        }
        public void Revoke()
        {
            if (IsRevoked)
                throw new DomainException("Token đã bị revoke trước đó.");

            IsRevoked = true;
            SetUpdated();
        }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}
