using AIResumeAnalyzer.Application.Exceptions;
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

namespace AIResumeAnalyzer.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly ICurrentUserService _currentUserService;

        private readonly IGenericRepository<RefreshToken>
            _refreshTokenRepository;

        private readonly IUnitOfWork _unitOfWork;

        public LogoutCommandHandler(
            ICurrentUserService currentUserService,
            IGenericRepository<RefreshToken> refreshTokenRepository,
            IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(
            LogoutCommand request,
            CancellationToken cancellationToken)
        {
            if (!_currentUserService.UserId.HasValue)
            {
                throw new UnauthorizedAccessException();
            }

            var refreshTokens = await _refreshTokenRepository.WhereAsync(
                x => x.UserId == _currentUserService.UserId.Value
                && !x.IsRevoked);

            foreach (var token in refreshTokens)
            {
                token.IsRevoked = true;

                _refreshTokenRepository.Update(token);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
