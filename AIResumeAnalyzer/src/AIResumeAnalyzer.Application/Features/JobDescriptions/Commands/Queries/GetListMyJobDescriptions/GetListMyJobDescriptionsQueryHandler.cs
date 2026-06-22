using AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Queries.GetListJobDescriptions;
using AIResumeAnalyzer.Application.Features.JobDescriptions.DTO;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Domain.Entities;
using MediatR;

namespace AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Queries.GetListMyJobDescriptions
{
    public class GetListMyJobDescriptionsQueryHandler : IRequestHandler<GetListMyJobDescriptionsQuery, List<JobDescriptionResponse>>
    {
        private readonly IGenericRepository<JobDescription> _jobDescriptionRepository;

        private readonly ICurrentUserService _currentUserService;

        public GetListMyJobDescriptionsQueryHandler(
            IGenericRepository<JobDescription> jobDescriptionRepository,
            ICurrentUserService currentUserService)
        {
            _jobDescriptionRepository = jobDescriptionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<JobDescriptionResponse>> Handle(GetListMyJobDescriptionsQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.UserId.HasValue)
            {
                throw new UnauthorizedAccessException();
            }

            var jobDescriptions = await _jobDescriptionRepository.WhereAsync(x => x.UserId == _currentUserService.UserId.Value);

            return jobDescriptions.OrderByDescending(x => x.CreatedAt)
                .Select(x => new JobDescriptionResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    CompanyName = x.CompanyName
                })
                .ToList();
        }
    }
}
