using AIResumeAnalyzer.Application.Features.Dashboard.DTOs;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Dashboard.Commands.Queries.GetDashboardCharts
{
    public class GetDashboardChartsQueryHandler : IRequestHandler<GetDashboardChartsQuery, DashboardChartsResponse>
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IJobMatchRepository _jobMatchRepository;
        private readonly IInterviewSessionRepository _interviewRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetDashboardChartsQueryHandler(
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

        public async Task<DashboardChartsResponse> Handle(GetDashboardChartsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            var resumes = await _resumeRepository.GetAllAsync(userId, cancellationToken);
            var jobMatches = await _jobMatchRepository.GetAllAsync(userId, cancellationToken);
            var interviews = await _interviewRepository.GetAllAsync(userId, cancellationToken);

            var response = new DashboardChartsResponse();

            #region ATS Trend

            response.AtsTrend = resumes
                .GroupBy(x => x.CreatedAt.Date)
                .OrderBy(x => x.Key)
                .Select(x => new ChartItemResponse
                {
                    Date = x.Key,
                    Label = x.Key.ToString("dd/MM"),
                    Value = Math.Round(x.Average(r => r.ATSScore), 2)
                })
                .ToList();

            #endregion

            #region Match Trend

            response.MatchTrend = jobMatches
                .GroupBy(x => x.CreatedAt.Date)
                .OrderBy(x => x.Key)
                .Select(x => new ChartItemResponse
                {
                    Date = x.Key,
                    Label = x.Key.ToString("dd/MM"),
                    Value = Math.Round(x.Average(m => m.MatchScore), 2)
                })
                .ToList();

            #endregion

            #region Interview Trend

            response.InterviewTrend = interviews
                .Where(x => x.IsCompleted && x.FinishedAt.HasValue)
                .GroupBy(x => x.FinishedAt!.Value.Date)
                .OrderBy(x => x.Key)
                .Select(x => new ChartItemResponse
                {
                    Date = x.Key,
                    Label = x.Key.ToString("dd/MM"),
                    Value = Math.Round(x.Average(i => i.Score), 2)
                })
                .ToList();

            #endregion

            return response;
        }
    }
}
