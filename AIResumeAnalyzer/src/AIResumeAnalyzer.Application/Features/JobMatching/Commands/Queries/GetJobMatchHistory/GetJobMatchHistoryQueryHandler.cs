using AIResumeAnalyzer.Application.Common.Pagination;
using AIResumeAnalyzer.Application.Features.JobMatching.DTO;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobMatching.Commands.Queries.GetJobMatchHistory
{
    public class GetJobMatchHistoryQueryHandler : IRequestHandler<GetJobMatchHistoryQuery, PagedResult<JobMatchHistoryItemResponse>>
    {
        private readonly IJobMatchRepository _jobMatchRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetJobMatchHistoryQueryHandler(
            IJobMatchRepository jobMatchRepository,
            ICurrentUserService currentUserService)
        {
            _jobMatchRepository = jobMatchRepository;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResult<JobMatchHistoryItemResponse>> Handle(GetJobMatchHistoryQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            return await _jobMatchRepository.GetHistoryAsync(userId, request.Filter, cancellationToken);
        }
    }
}
