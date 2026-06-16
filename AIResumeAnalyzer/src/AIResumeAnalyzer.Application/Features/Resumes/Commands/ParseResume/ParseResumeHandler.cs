using AIResumeAnalyzer.Application.Common.Exceptions;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IFileService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Application.Interfaces.Persistence;
using AIResumeAnalyzer.Domain.Entities;
using AIResumeAnalyzer.Domain.Enums;
using MediatR;

namespace AIResumeAnalyzer.Application.Features.Resumes.Commands.ParseResume
{
    public class ParseResumeCommandHandler : IRequestHandler<ParseResumeCommand, bool>
    {
        private readonly IGenericRepository<Resume> _resumeRepository;

        private readonly IFileStorageService _fileStorageService;

        private readonly IResumeParserService _resumeParserService;

        private readonly ICurrentUserService _currentUserService;

        private readonly IUnitOfWork _unitOfWork;

        public ParseResumeCommandHandler(
            IGenericRepository<Resume> resumeRepository,
            IFileStorageService fileStorageService,
            IResumeParserService resumeParserService,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _resumeRepository = resumeRepository;
            _fileStorageService = fileStorageService;
            _resumeParserService = resumeParserService;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(
            ParseResumeCommand request,
            CancellationToken cancellationToken)
        {
            var resume = await _resumeRepository.FirstOrDefaultAsync(x => x.Id == request.ResumeId && x.UserId == _currentUserService.UserId);

            if (resume is null)
            {
                throw new NotFoundException(
                    "Resume not found.");
            }

            try
            {
                resume.Status = ResumeStatus.Processing;

                _resumeRepository.Update(resume);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var physicalPath = _fileStorageService.GetPhysicalPath(resume.FileUrl);

                var parsedText = await _resumeParserService.ParsePdfAsync(physicalPath, cancellationToken);

                resume.ParsedText = parsedText;

                resume.Status = ResumeStatus.Completed;

                _resumeRepository.Update(resume);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch
            {
                resume.Status = ResumeStatus.Failed;

                _resumeRepository.Update(resume);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                throw;
            }
        }
    }
}
