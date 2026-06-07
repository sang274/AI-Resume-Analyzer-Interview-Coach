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

namespace AIResumeAnalyzer.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(
            IGenericRepository<User> userRepository,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterResponse> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var existingUser =
                await _userRepository.FirstOrDefaultAsync(
                    x => x.Email == request.Email);

            if (existingUser is not null)
            {
                throw new BadRequestException(
                    "Email already exists.");
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash =
                    _passwordHasher.HashPassword(
                        request.Password)
            };

            await _userRepository.AddAsync(user);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new RegisterResponse
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName
            };
        }
    }
}
