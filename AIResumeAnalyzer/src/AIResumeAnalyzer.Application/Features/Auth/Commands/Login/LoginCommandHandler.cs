using AIResumeAnalyzer.Application.Exceptions;
using AIResumeAnalyzer.Application.Features.Auth.DTOs;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
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
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(
            IGenericRepository<User> userRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user =
                await _userRepository.FirstOrDefaultAsync(
                    x => x.Email == request.Email);

            if (user is null)
            {
                throw new BadRequestException(
                    "Invalid email or password.");
            }

            var isValidPassword =
                _passwordHasher.VerifyPassword(
                    request.Password,
                    user.PasswordHash);

            if (!isValidPassword)
            {
                throw new BadRequestException(
                    "Invalid email or password.");
            }

            var token =
                _jwtService.GenerateAccessToken(user);

            return new LoginResponse
            {
                AccessToken = token,
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email
            };
        }
    }
}
