using MediatR;
using MyPortfolio.Application.Features.Auth.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Domain.Interfaces.Services;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, LoginResult>
    {
        private readonly IUserRepository _userRepository;   
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenHandler(
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

        public async Task<LoginResult> Handle(RefreshTokenCommand request, CancellationToken ct)
        {
            var oldToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, ct) 
                ?? throw new DomainException("Refresh token không hợp lệ");

            var user = await _userRepository.GetByIdAsync(oldToken.UserId, ct)
                ?? throw new NotFoundException("Người dùng không tồn tại");

            //revoke token
            oldToken.Revoke();
            await _refreshTokenRepository.UpdateAsync(oldToken, ct);

            var newAccessToken = _jwtTokenService.GenerateAccessToken(user.Id, user.Role);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();
            var newTokenEntity = ReFreshToken.Create(user.Id, newRefreshToken);
            await _refreshTokenRepository.AddAsync(newTokenEntity, ct); 
            await _unitOfWork.SaveChangesAsync(ct);

            return new LoginResult(
                AccessToken: newAccessToken,
                RefreshToken: newRefreshToken,
                AccessTokenExpiry: DateTime.UtcNow.AddMinutes(15)
            );
        }
    }
}
