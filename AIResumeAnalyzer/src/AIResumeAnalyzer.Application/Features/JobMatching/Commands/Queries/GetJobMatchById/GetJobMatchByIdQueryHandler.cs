using AIResumeAnalyzer.Application.Common.Exceptions;
using AIResumeAnalyzer.Application.Features.JobMatching.DTO;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobMatching.Commands.Queries.GetJobMatchById
{
    public class GetJobMatchByIdQueryHandler : IRequestHandler<GetJobMatchByIdQuery, JobMatchDetailResponse>
    {
        private readonly IJobMatchRepository _jobMatchRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetJobMatchByIdQueryHandler(
            IJobMatchRepository jobMatchRepository, 
            ICurrentUserService currentUserService)
        {
            _jobMatchRepository = jobMatchRepository;
            _currentUserService = currentUserService;
        }

        public async Task<JobMatchDetailResponse> Handle(GetJobMatchByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            var match = await _jobMatchRepository.GetDetailByIdAsync(request.JobMatchId, userId, cancellationToken);

            if (match is null)
            {
                throw new NotFoundException("Job match not found.");
            }

            return new JobMatchDetailResponse
            {
                Id = match.Id,

                ResumeId = match.ResumeId,

                JobDescriptionId = match.JobDescriptionId,

                MatchScore = match.MatchScore,

                MissingKeywords = match.MissingKeywords,

                MatchedKeywords = match.MatchedKeywords,

                Suggestions = match.Suggestions,

                JobTitle = match.JobDescription.Title,

                CompanyName = match.JobDescription.CompanyName,

                CreatedAt = match.CreatedAt
            };
        }
    }
}
