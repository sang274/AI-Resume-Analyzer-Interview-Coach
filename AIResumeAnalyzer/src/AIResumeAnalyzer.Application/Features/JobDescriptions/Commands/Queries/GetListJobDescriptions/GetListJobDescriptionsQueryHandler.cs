using AIResumeAnalyzer.Application.Features.JobDescriptions.DTO;
using AIResumeAnalyzer.Application.Features.Resumes.DTOs;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Queries.GetListJobDescriptions
{
    public class GetListJobDescriptionsQueryHandler : IRequestHandler<GetListJobDescriptionsQuery, List<JobDescriptionResponse>>
    {
        private readonly IGenericRepository<JobDescription> _jobDescriptionRepository;

        private readonly ICurrentUserService _currentUserService;

        public GetListJobDescriptionsQueryHandler(
            IGenericRepository<JobDescription> jobDescriptionRepository,
            ICurrentUserService currentUserService)
        {
            _jobDescriptionRepository = jobDescriptionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<JobDescriptionResponse>> Handle(GetListJobDescriptionsQuery request, CancellationToken cancellationToken)
        {
            var jobDescriptions = await _jobDescriptionRepository.GetAllAsync();

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
