using MediatR;
using MyPortfolio.Application.Features.Auth.DTOs;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Domain.Interfaces.Services;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Application.Features.Auth.Commands.Logout
{
    public class LogoutHandler : IRequestHandler<LogoutCommand> 
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogoutHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork )
        {
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(LogoutCommand request, CancellationToken ct)
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, ct)
                ?? throw new DomainException("token không hợp lệ");
            
            token.Revoke();
            await _refreshTokenRepository.UpdateAsync(token, ct);
            await _unitOfWork.SaveChangesAsync();
        }
  
    }
}
