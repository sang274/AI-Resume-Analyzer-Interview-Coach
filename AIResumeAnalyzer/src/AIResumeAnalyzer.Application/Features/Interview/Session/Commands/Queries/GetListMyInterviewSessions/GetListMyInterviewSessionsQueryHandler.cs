using AIResumeAnalyzer.Application.Features.Interview.Session.DTO;
using AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Queries.GetListMyJobDescriptions;
using AIResumeAnalyzer.Application.Features.JobDescriptions.DTO;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Domain.Entities;
using AIResumeAnalyzer.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Session.Commands.Queries.GetInterviewSessions
{
    public class GetListMyInterviewSessionsQueryHandler : IRequestHandler<GetListMyInterviewSessionsQuery, List<InterviewSessionListItemResponse>>
    {
        private readonly IInterviewSessionRepository _interviewSessionRepository;

        private readonly ICurrentUserService _currentUserService;

        public GetListMyInterviewSessionsQueryHandler(
        IInterviewSessionRepository jobDescriptionRepository,
        ICurrentUserService currentUserService)
        {
            _interviewSessionRepository = jobDescriptionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<InterviewSessionListItemResponse>> Handle(GetListMyInterviewSessionsQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.UserId.HasValue)
            {
                throw new UnauthorizedAccessException();
            }

            var sessions = await _interviewSessionRepository.GetMySessionsAsync(_currentUserService.UserId.Value, cancellationToken);

            var response = sessions.Select(session => new InterviewSessionListItemResponse
            {
                Id = session.Id,
                JobTitle = session.JobDescription?.Title ?? "N/A",
                CompanyName = session.JobDescription?.CompanyName ?? "N/A",
                Type = session.Type,
                Score = session.Score,
                QuestionCount = session.Questions != null ? session.Questions.Count : 0,
                IsCompleted = session.IsCompleted,
                StartedAt = session.StartedAt,
                FinishedAt = session.FinishedAt
            }).ToList();

            return response;
        }
    }
}