using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IFileService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Application.Interfaces.Persistence;
using AIResumeAnalyzer.Domain.Entities;
using AIResumeAnalyzer.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Auth.Commands.UploadResume
{
    public class UploadResumeCommandHandler : IRequestHandler<UploadResumeCommand, Guid>
    {
        private readonly IGenericRepository<Resume>  _resumeRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public UploadResumeCommandHandler(
            IGenericRepository<Resume> resumeRepository,
            IFileStorageService fileStorageService,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _resumeRepository = resumeRepository;
            _fileStorageService = fileStorageService;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UploadResumeCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.UserId.HasValue)
            {
                throw new UnauthorizedAccessException();
            }

            var fileUrl = await _fileStorageService.SaveResumeAsync(request.File, cancellationToken);

            var resume = new Resume
            {
                UserId = _currentUserService.UserId.Value,

                FileName = request.File.FileName,

                FileUrl = fileUrl,

                ParsedText = string.Empty,

                ATSScore = 0,

                Status = ResumeStatus.Pending
            };

            await _resumeRepository.AddAsync(resume);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return resume.Id;
        }
    }
}
