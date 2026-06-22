using MediatR;
using MyPortfolio.Application.Features.Auth.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Domain.Interfaces.Services;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Entities;
namespace MyPortfolio.Application.Features.Auth.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenService _jwtTokenService ;
        private readonly IUnitOfWork _unitOfWork;

        public LoginHandler(
            IUserRepository userRepository, 
            IRefreshTokenRepository refreshTokenRepository, 
            IJwtTokenService jwtTokenService, 
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenService = jwtTokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken ct)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, ct);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new DomainException("Email hoặc mật khẩu không đúng");
            }
            // tạo token
            var accessToken = _jwtTokenService.GenerateAccessToken(user.Id, user.Role);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            // lưu refreshtoken
            var tokenEntity = ReFreshToken.Create(user.Id, refreshToken);   
            await _refreshTokenRepository.AddAsync(tokenEntity);
            await _unitOfWork.SaveChangesAsync();

            return new LoginResult
            (
               AccessToken: accessToken,
               RefreshToken: refreshToken,
               AccessTokenExpiry: DateTime.UtcNow.AddMinutes(15)
            );
        }
    }
}
