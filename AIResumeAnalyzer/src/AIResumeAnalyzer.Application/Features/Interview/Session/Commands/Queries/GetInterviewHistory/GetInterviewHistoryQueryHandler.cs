using AIResumeAnalyzer.Application.Common.Pagination;
using AIResumeAnalyzer.Application.Features.Interview.Session.DTO;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Session.Commands.Queries.GetInterviewHistory
{
    public class GetInterviewHistoryQueryHandler : IRequestHandler<GetInterviewHistoryQuery, PagedResult<InterviewHistoryItemResponse>>
    {
        private readonly IInterviewSessionRepository _interviewRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetInterviewHistoryQueryHandler(
            IInterviewSessionRepository interviewRepository,
            ICurrentUserService currentUserService)
        {
            _interviewRepository = interviewRepository;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResult<InterviewHistoryItemResponse>> Handle(GetInterviewHistoryQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            return await _interviewRepository.GetHistoryAsync(userId, request.Filter, cancellationToken);
        }
    }
}
