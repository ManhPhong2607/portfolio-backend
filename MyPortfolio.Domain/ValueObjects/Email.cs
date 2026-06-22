using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Domain.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; }

        public Email(string value) => Value = value;

        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email không được rỗng.");

            var normalized = email.Trim().ToLowerInvariant();

            if (!Regex.IsMatch(normalized, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new DomainException($"Email '{email}' không hợp lệ.");
            return new Email(normalized);
        }

        public override string ToString() => Value;
        public override bool Equals(object? obj) => obj is Email other && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}
