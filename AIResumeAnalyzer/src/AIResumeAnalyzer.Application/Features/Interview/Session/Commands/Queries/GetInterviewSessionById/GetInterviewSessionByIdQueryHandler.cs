using AIResumeAnalyzer.Application.Common.Exceptions;
using AIResumeAnalyzer.Application.Features.Interview.Question.DTO;
using AIResumeAnalyzer.Application.Features.Interview.Session.DTO;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Session.Commands.Queries.GetInterviewSessionById
{
    public class GetInterviewSessionByIdQueryHandler : IRequestHandler<GetInterviewSessionByIdQuery, InterviewSessionResponse>
    {
        private readonly IInterviewSessionRepository _repository;

        private readonly ICurrentUserService _currentUserService;

        public GetInterviewSessionByIdQueryHandler(
            IInterviewSessionRepository repository,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<InterviewSessionResponse> Handle(GetInterviewSessionByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            var session = await _repository.GetDetailAsync(
                    request.SessionId,
                    userId,
                    cancellationToken);

            if (session is null)
            {
                throw new NotFoundException("Interview session not found.");
            }

            return new InterviewSessionResponse
            {
                Id = session.Id,

                ResumeId = session.ResumeId,

                JobDescriptionId = session.JobDescriptionId,

                Type = session.Type,

                Score = session.Score,

                StartedAt = session.StartedAt,

                FinishedAt = session.FinishedAt,

                Questions = session.Questions.OrderBy(x => x.CreatedAt)
                    .Select(x =>
                        new InterviewQuestionResponse
                        {
                            Id = x.Id,

                            Question = x.Question,

                            Answer = x.Answer,

                            AIResponse = x.AIResponse,

                            Score = x.Score
                        })
                    .ToList(),

                OverallFeedback = session.OverallFeedback,

                Strengths = session.Strengths,

                Weaknesses = session.Weaknesses,

                ImprovementSuggestions = session.ImprovementSuggestions,

                IsCompleted = session.IsCompleted
            };
        }
    }
}
