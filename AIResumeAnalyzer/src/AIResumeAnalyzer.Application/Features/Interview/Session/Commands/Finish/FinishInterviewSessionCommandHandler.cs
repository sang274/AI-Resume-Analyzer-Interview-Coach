using AIResumeAnalyzer.Application.Common.Exceptions;
using AIResumeAnalyzer.Application.Exceptions;
using AIResumeAnalyzer.Application.Features.Interview.Session.DTO;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IInterviewService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Session.Commands.Finish
{
    public class FinishInterviewSessionCommandHandler
    : IRequestHandler<
        FinishInterviewSessionCommand,
        InterviewSummaryResponse>
    {
        private readonly IInterviewSessionRepository _interviewSessionRepository;
        private readonly IInterviewCoachService _interviewCoachService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public FinishInterviewSessionCommandHandler(
            IInterviewSessionRepository interviewSessionRepository,
            IInterviewCoachService interviewCoachService,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _interviewSessionRepository = interviewSessionRepository;
            _interviewCoachService = interviewCoachService;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<InterviewSummaryResponse> Handle(
            FinishInterviewSessionCommand request,
            CancellationToken cancellationToken)
        {
            var userId =
                _currentUserService.UserId
                ?? throw new UnauthorizedAccessException();

            var session =
                await _interviewSessionRepository.GetForFinishAsync(
                    request.SessionId,
                    userId,
                    cancellationToken);

            if (session is null)
            {
                throw new NotFoundException(
                    "Interview session not found.");
            }

            if (session.IsCompleted)
            {
                throw new BadRequestException(
                    "Interview session has already been completed.");
            }

            if (!session.Questions.Any())
            {
                throw new BadRequestException(
                    "Interview session contains no questions.");
            }

            var unansweredQuestions = session.Questions
                .Where(x => string.IsNullOrWhiteSpace(x.Answer))
                .ToList();

            if (unansweredQuestions.Any())
            {
                throw new BadRequestException(
                    $"Please answer all questions before finishing the interview. Remaining: {unansweredQuestions.Count}");
            }

            session.Score = session.Questions.Average(x => x.Score);

            session.FinishedAt = DateTime.UtcNow;

            session.IsCompleted = true;

            var summary = await _interviewCoachService.SummarizeInterviewAsync(session.Questions, cancellationToken);

            session.OverallFeedback = summary.OverallFeedback;

            session.Strengths = summary.Strengths;

            session.Weaknesses = summary.Weaknesses;

            session.ImprovementSuggestions = summary.ImprovementSuggestions;

            _interviewSessionRepository.Update(session);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new InterviewSummaryResponse
            {
                AverageScore = session.Score,
                OverallFeedback = session.OverallFeedback!,
                Strengths = session.Strengths!,
                Weaknesses = session.Weaknesses!,
                ImprovementSuggestions = session.ImprovementSuggestions!
            };
        }
    }
}
