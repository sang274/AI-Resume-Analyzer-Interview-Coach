using AIResumeAnalyzer.Application.Common.Exceptions;
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

namespace AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Queries.GetJobDescriptionById
{
    public class GetJobDescriptionByIdQueryHandler : IRequestHandler<GetJobDescriptionByIdQuery, JobDescriptionResponseDetail>
    {
        private readonly IGenericRepository<JobDescription> _jobDescriptionRepository;

        private readonly ICurrentUserService _currentUserService;

        public GetJobDescriptionByIdQueryHandler(
            IGenericRepository<JobDescription> jobDescriptionRepository,
            ICurrentUserService currentUserService)
        {
            _jobDescriptionRepository = jobDescriptionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<JobDescriptionResponseDetail> Handle(GetJobDescriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var jobDescription = await _jobDescriptionRepository.FirstOrDefaultAsync(
                    x => x.Id == request.JobDescriptionId);

            if (jobDescription is null)
            {
                throw new NotFoundException("Job description not found.");
            }

            return new JobDescriptionResponseDetail
            {
                Id = jobDescription.Id,
                UserId = jobDescription.UserId,
                Title = jobDescription.Title,
                CompanyName = jobDescription.CompanyName,
                Content = jobDescription.Content,
                CreatedAt = jobDescription.CreatedAt
            };
        }
    }
}
