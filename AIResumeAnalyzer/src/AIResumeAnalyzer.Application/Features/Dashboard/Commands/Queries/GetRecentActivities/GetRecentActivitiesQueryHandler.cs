using AIResumeAnalyzer.Application.Features.Dashboard.DTOs;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Dashboard.Commands.Queries.GetRecentActivities
{
    public class GetRecentActivitiesQueryHandler : IRequestHandler<GetRecentActivitiesQuery, List<RecentActivityResponse>>
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IJobMatchRepository _jobMatchRepository;
        private readonly IInterviewSessionRepository _interviewRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetRecentActivitiesQueryHandler(
            IResumeRepository resumeRepository,
            IJobMatchRepository jobMatchRepository,
            IInterviewSessionRepository interviewRepository,
            ICurrentUserService currentUserService)
        {
            _resumeRepository = resumeRepository;
            _jobMatchRepository = jobMatchRepository;
            _interviewRepository = interviewRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<RecentActivityResponse>> Handle(GetRecentActivitiesQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            var resumes = await _resumeRepository.GetRecentAsync(userId, request.Take, cancellationToken);

            var jobMatches = await _jobMatchRepository.GetRecentAsync(userId, request.Take, cancellationToken);

            var interviews = await _interviewRepository.GetRecentAsync(userId, request.Take, cancellationToken);

            var activities = new List<RecentActivityResponse>();

            // Resume Uploaded
            activities.AddRange(resumes.Select(x => new RecentActivityResponse
                {
                    Type = ActivityType.ResumeUploaded,
                    ReferenceId = x.Id,
                    Title = "Resume Uploaded",
                    Description = x.FileName,
                    CreatedAt = x.CreatedAt
                }));

            // Resume Analyzed
            activities.AddRange(resumes.Where(x => x.Status == ResumeStatus.Completed).Select(x => new RecentActivityResponse
                    {
                        Type = ActivityType.ResumeAnalyzed,
                        ReferenceId = x.Id,
                        Title = "Resume Analyzed",
                        Description = $"ATS Score: {x.ATSScore:F1}",
                        CreatedAt = x.UpdatedAt ?? x.CreatedAt
                    }));

            // Job Match
            activities.AddRange(jobMatches.Select(x => new RecentActivityResponse
                {
                    Type = ActivityType.JobMatched,
                    ReferenceId = x.Id,
                    Title = "Job Matched",
                    Description =$"{x.JobDescription.CompanyName} - {x.JobDescription.Title} ({x.MatchScore:F1}%)",
                    CreatedAt = x.CreatedAt
                }));

            // Interview Started
            activities.AddRange(interviews.Select(x => new RecentActivityResponse
                {
                    Type = ActivityType.InterviewStarted,
                    ReferenceId = x.Id,
                    Title = "Interview Started",
                    Description =$"{x.JobDescription.CompanyName} - {x.JobDescription.Title}",
                    CreatedAt = x.StartedAt
                }));

            // Interview Completed
            activities.AddRange(interviews.Where(x => x.IsCompleted).Select(x => new RecentActivityResponse
                    {
                        Type = ActivityType.InterviewCompleted,
                        ReferenceId = x.Id,
                        Title = "Interview Completed",
                        Description = $"Average Score: {x.Score:F1}",
                        CreatedAt = x.FinishedAt ?? x.CreatedAt
                    }));

            return activities.OrderByDescending(x => x.CreatedAt).Take(request.Take).ToList();
        }
    }
}
