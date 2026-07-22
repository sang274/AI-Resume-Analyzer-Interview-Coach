using AIResumeAnalyzer.Application.Common.Exceptions;
using AIResumeAnalyzer.Application.Exceptions;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IInterviewService;
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

namespace AIResumeAnalyzer.Application.Features.Interview.Question.Commands.Create
{
    public class GenerateInterviewQuestionsCommandHandler : IRequestHandler<GenerateInterviewQuestionsCommand, Guid>
    {
        private readonly IGenericRepository<Resume> _resumeRepository;

        private readonly IGenericRepository<JobDescription> _jobDescriptionRepository;

        private readonly IGenericRepository<InterviewSession> _interviewSessionRepository;

        private readonly IGenericRepository<InterviewQuestion> _interviewQuestionRepository;

        private readonly IInterviewCoachService _interviewCoachService;

        private readonly ICurrentUserService _currentUserService;

        private readonly IUnitOfWork _unitOfWork;

        public GenerateInterviewQuestionsCommandHandler(
            IGenericRepository<Resume> resumeRepository,
            IGenericRepository<JobDescription> jobDescriptionRepository,
            IGenericRepository<InterviewSession> interviewSessionRepository,
            IGenericRepository<InterviewQuestion> interviewQuestionRepository,
            IInterviewCoachService interviewCoachService,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _resumeRepository = resumeRepository;
            _jobDescriptionRepository = jobDescriptionRepository;
            _interviewSessionRepository = interviewSessionRepository;
            _interviewQuestionRepository = interviewQuestionRepository;
            _interviewCoachService = interviewCoachService;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(GenerateInterviewQuestionsCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            var resume = await _resumeRepository.FirstOrDefaultAsync(x => x.Id == request.ResumeId && x.UserId == userId);

            if (resume is null)
            {
                throw new NotFoundException("Resume not found.");
            }

            if (string.IsNullOrWhiteSpace(resume.ParsedText))
            {
                throw new BadRequestException("Resume has not been parsed.");
            }

            var jobDescription = await _jobDescriptionRepository.FirstOrDefaultAsync(x => x.Id == request.JobDescriptionId);

            if (jobDescription is null)
            {
                throw new NotFoundException("Job description not found.");
            }

            var aiResult = await _interviewCoachService.GenerateQuestionsAsync(resume.ParsedText, jobDescription.Content, cancellationToken);

            if (aiResult.Questions.Count == 0)
            {
                throw new BadRequestException("No interview questions generated.");
            }

            var session = new InterviewSession
                {
                    UserId = userId,

                    ResumeId = resume.Id,

                    JobDescriptionId = jobDescription.Id,

                    Type = InterviewType.Technical,

                    Score = 0,

                    StartedAt = DateTime.UtcNow,

                    IsCompleted = false
            };

            await _interviewSessionRepository.AddAsync(session);

            foreach (var question in aiResult.Questions)
            {
                var interviewQuestion = new InterviewQuestion
                    {
                        InterviewSessionId = session.Id,

                        Question = question,

                        Score = 0
                    };

                await _interviewQuestionRepository.AddAsync(interviewQuestion);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return session.Id;
        }
    }
}
