using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Domain.Interfaces.Services;
using MyPortfolio.Application.Features.Auth.DTOs;
using System.Net.WebSockets;

namespace MyPortfolio.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordHandler(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtTokenService jwtTokenService,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenService = jwtTokenService;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ChangePasswordCommand request, CancellationToken ct)
        {
            //var user = await _userRepository.GetByIdAsync(_currentUserService.userId, ct);

            var user = await _userRepository.GetByIdAsync(_currentUserService.UserId, ct)
              ?? throw new NotFoundException("Người dùng không tồn tại");

            //var user = await _userRepository.GetByIdAsync(_currentUserService.UserId, ct)
            //    ?? throw new DomainException("Người dùng không tồn tại");

            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash))          
                throw new DomainException("Mật khẩu cũ không đúng");

            //hash password mới
            var newHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword, workFactor: 12);
            user.UpdatePasswordHash(newHash);
            await _userRepository.UpdateAsync(user, ct);

            //revoke tất cả refresh token 
            await _refreshTokenRepository.RevokeAllByUserAsync(user.Id, ct);
            await _unitOfWork.SaveChangesAsync(ct);

        }

    }
}
