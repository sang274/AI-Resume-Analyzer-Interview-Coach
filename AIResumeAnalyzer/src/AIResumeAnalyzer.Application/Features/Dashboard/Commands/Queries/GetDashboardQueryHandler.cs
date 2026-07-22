using AIResumeAnalyzer.Application.Features.Dashboard.DTOs;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Dashboard.Commands.Queries
{
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardResponse>
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IJobMatchRepository _jobMatchRepository;
        private readonly IInterviewSessionRepository _interviewRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetDashboardQueryHandler(
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

        public async Task<DashboardResponse> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            var response = new DashboardResponse();

            response.Resume.Total = await _resumeRepository.GetCountAsync(userId, cancellationToken);

            response.Resume.AverageATSScore = await _resumeRepository.GetAverageATSScoreAsync(userId, cancellationToken);

            response.Resume.BestATSScore = await _resumeRepository.GetBestATSScoreAsync(userId, cancellationToken);

            response.JobMatch.Total = await _jobMatchRepository.GetCountAsync(userId, cancellationToken);

            response.JobMatch.AverageMatchScore = await _jobMatchRepository.GetAverageScoreAsync(userId, cancellationToken);

            response.JobMatch.BestMatchScore = await _jobMatchRepository.GetBestScoreAsync(userId, cancellationToken);

            response.Interview.Total = await _interviewRepository.GetCountAsync(userId, cancellationToken);

            response.Interview.Completed = await _interviewRepository.GetCompletedCountAsync(userId, cancellationToken);

            response.Interview.AverageInterviewScore = await _interviewRepository.GetAverageScoreAsync(userId, cancellationToken);

            response.Interview.BestInterviewScore = await _interviewRepository.GetBestScoreAsync(userId, cancellationToken);

            return response;
        }
    }
}
