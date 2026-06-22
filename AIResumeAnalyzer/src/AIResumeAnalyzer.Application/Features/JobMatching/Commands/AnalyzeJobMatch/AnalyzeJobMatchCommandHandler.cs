using AIResumeAnalyzer.Application.Common.Exceptions;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IJobService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Application.Interfaces.Persistence;
using AIResumeAnalyzer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobMatching.Commands.AnalyzeJobMatch
{
    public class AnalyzeJobMatchCommandHandler : IRequestHandler<AnalyzeJobMatchCommand, Guid>
    {
        private readonly IGenericRepository<Resume> _resumeRepository;

        private readonly IGenericRepository<JobDescription> _jobDescriptionRepository;

        private readonly IGenericRepository<JobMatch> _jobMatchRepository;

        private readonly IJobMatchingService _jobMatchingService;

        private readonly ICurrentUserService _currentUserService;

        private readonly IUnitOfWork _unitOfWork;

        public AnalyzeJobMatchCommandHandler(
            IGenericRepository<Resume> resumeRepository,
            IGenericRepository<JobDescription> jobDescriptionRepository,
            IGenericRepository<JobMatch> jobMatchRepository,
            IJobMatchingService jobMatchingService,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _resumeRepository = resumeRepository;
            _jobDescriptionRepository = jobDescriptionRepository;
            _jobMatchRepository = jobMatchRepository;
            _jobMatchingService = jobMatchingService;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AnalyzeJobMatchCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            var resume = await _resumeRepository.FirstOrDefaultAsync(x => x.Id == request.ResumeId && x.UserId == userId);

            if (resume is null)
            {
                throw new NotFoundException("Resume not found.");
            }

            var jobDescription = await _jobDescriptionRepository.FirstOrDefaultAsync(x => x.Id == request.JobDescriptionId);

            if (jobDescription is null)
            {
                throw new NotFoundException("Job description not found.");
            }

            var result = await _jobMatchingService.AnalyzeAsync(
                        resume.ParsedText,
                        jobDescription.Content,
                        cancellationToken);

            var jobMatch = new JobMatch
                {
                    ResumeId = resume.Id,

                    JobDescriptionId = jobDescription.Id,

                    MatchScore = result.MatchScore,

                    MissingKeywords = result.MissingKeywords
                };

            await _jobMatchRepository.AddAsync(jobMatch);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return jobMatch.Id;
        }
    }
}
