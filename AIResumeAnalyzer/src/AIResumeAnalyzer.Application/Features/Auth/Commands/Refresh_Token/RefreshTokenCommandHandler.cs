using AIResumeAnalyzer.Application.Exceptions;
using AIResumeAnalyzer.Application.Features.Auth.DTOs;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Application.Interfaces.Persistence;
using AIResumeAnalyzer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Auth.Commands.Refresh_Token
{
    public class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand, TokenResponse>
    {
        private readonly IGenericRepository<RefreshToken> _refreshTokenRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenCommandHandler(
            IGenericRepository<RefreshToken> refreshTokenRepository,
            IGenericRepository<User> userRepository,
            IJwtService jwtService,
            IUnitOfWork unitOfWork)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<TokenResponse> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var storedToken =
                await _refreshTokenRepository.FirstOrDefaultAsync(x => x.Token == request.RefreshToken);

            if (storedToken is null)
            {
                throw new BadRequestException("Invalid refresh token.");
            }

            if (storedToken.IsRevoked)
            {
                throw new BadRequestException("Refresh token revoked.");
            }

            if (storedToken.ExpiredAt <= DateTime.UtcNow)
            {
                throw new BadRequestException("Refresh token expired.");
            }

            var user = await _userRepository.GetByIdAsync(storedToken.UserId);

            if (user is null)
            {
                throw new BadRequestException("User not found.");
            }

            var newAccessToken = _jwtService.GenerateAccessToken(user);

            var newRefreshToken = _jwtService.GenerateRefreshToken();

            storedToken.IsRevoked = true;

            _refreshTokenRepository.Update(storedToken);

            await _refreshTokenRepository.AddAsync(
                new RefreshToken
                {
                    UserId = user.Id,
                    Token = newRefreshToken,
                    ExpiredAt = DateTime.UtcNow.AddDays(7),
                    IsRevoked = false
                });

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
