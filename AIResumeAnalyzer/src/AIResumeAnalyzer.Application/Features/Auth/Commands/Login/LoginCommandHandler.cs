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


namespace AIResumeAnalyzer.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler
    : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<RefreshToken> _refreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(
            IGenericRepository<User> userRepository,
            IGenericRepository<RefreshToken> refreshTokenRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user is null)
            {
                throw new BadRequestException(
                    "Invalid email or password.");
            }

            var isValidPassword = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

            if (!isValidPassword)
            {
                throw new BadRequestException(
                    "Invalid email or password.");
            }

            var accessToken = _jwtService.GenerateAccessToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken();

            var refreshTokenEntity =
                new RefreshToken
                {
                    UserId = user.Id,
                    Token = refreshToken,
                    ExpiredAt = DateTime.UtcNow.AddDays(7)
                };

            await _refreshTokenRepository
                .AddAsync(refreshTokenEntity);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email
            };
        }
    }
}
