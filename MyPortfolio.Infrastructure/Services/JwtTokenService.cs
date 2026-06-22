
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MyPortfolio.Domain.Interfaces.Services;

namespace MyPortfolio.Infrastructure.Services;

public class JwtTokenService(IConfiguration configuration) : IJwtTokenService
{
    private readonly string _secret
        = configuration["Jwt:Secret"]
          ?? throw new InvalidOperationException("Jwt:Secret chưa được cấu hình.");

    private readonly string _issuer
        = configuration["Jwt:Issuer"]
          ?? throw new InvalidOperationException("Jwt:Issuer chưa được cấu hình.");

    private readonly string _audience
        = configuration["Jwt:Audience"]
          ?? throw new InvalidOperationException("Jwt:Audience chưa được cấu hình.");

    private readonly int _expiryMinutes
        = int.Parse(configuration["Jwt:AccessTokenExpiryMinutes"] ?? "15");

    public string GenerateAccessToken(Guid userId, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }

    public Guid? GetUserIdFromToken(string token)
    {
        try
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var handler = new JwtSecurityTokenHandler();

            handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = false // bỏ check expiry khi chỉ lấy userId
            }, out var validated);

            var jwt = (JwtSecurityToken)validated;
            var sub = jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
            return Guid.Parse(sub);
        }
        catch
        {
            return null;
        }
    }
}