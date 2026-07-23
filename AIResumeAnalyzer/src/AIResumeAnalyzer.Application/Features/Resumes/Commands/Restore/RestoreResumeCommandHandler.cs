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

namespace AIResumeAnalyzer.Application.Features.Resumes.Commands.Restore
{
    public class RestoreResumeCommandHandler : IRequestHandler<RestoreResumeCommand>
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IGenericRepository<Resume> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public RestoreResumeCommandHandler(
            IResumeRepository resumeRepository,
            IGenericRepository<Resume> genericRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _resumeRepository = resumeRepository;
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task Handle(
            RestoreResumeCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException();

            var resume = await _resumeRepository.GetDeletedAsync(
                request.ResumeId,
                userId,
                cancellationToken);

            if (resume == null)
                throw new KeyNotFoundException("Deleted resume not found.");

            _genericRepository.Restore(resume);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
