using AIResumeAnalyzer.Application.Common.Pagination;
using AIResumeAnalyzer.Application.Features.Resumes.DTOs;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Resumes.Commands.Queries.GetResumeHistory
{
    public class GetResumeHistoryQueryHandler : IRequestHandler<GetResumeHistoryQuery, PagedResult<ResumeHistoryItemResponse>>
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetResumeHistoryQueryHandler(
            IResumeRepository resumeRepository,
            ICurrentUserService currentUserService)
        {
            _resumeRepository = resumeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResult<ResumeHistoryItemResponse>> Handle(GetResumeHistoryQuery request,CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            return await _resumeRepository.GetHistoryAsync(userId, request.Filter, cancellationToken);
        }
    }
}
