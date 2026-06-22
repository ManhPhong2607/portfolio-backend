using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.ValueObjects;
namespace MyPortfolio.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public Email Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "admin";
        public DateTime? LastLoginAt { get; set; }

        private User() { }

        public static User Create(string username, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new DomainException("Username không được rỗng.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("PasswordHash không được rỗng.");

            return new User
            {
                Username = username.Trim(),
                Email = Email.Create(email),
                PasswordHash = passwordHash,
                Role = "admin"
            };
        }

        public void UpdatePasswordHash(string newHash)
        {
            if (string.IsNullOrWhiteSpace(newHash))
                throw new DomainException("PasswordHash mới không được rỗng.");

            PasswordHash = newHash;
            SetUpdated();
        }

        public void RecordLogin() => LastLoginAt = DateTime.UtcNow;
    }
}
