using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Application.Interfaces.Persistence;
using AIResumeAnalyzer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobDescriptions.Commands
{
    public class CreateJobDescriptionCommandHandler : IRequestHandler<CreateJobDescriptionCommand, Guid>
    {
        private readonly IGenericRepository<JobDescription> _jobDescriptionRepository;

        private readonly ICurrentUserService _currentUserService;

        private readonly IUnitOfWork _unitOfWork;

        public CreateJobDescriptionCommandHandler(
            IGenericRepository<JobDescription> jobDescriptionRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _jobDescriptionRepository = jobDescriptionRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateJobDescriptionCommand request, CancellationToken cancellationToken)
        {
            var entity =
                new JobDescription
                {
                    UserId = _currentUserService.UserId!.Value,

                    Title = request.Title,

                    CompanyName = request.CompanyName,

                    Content = request.Content
                };

            await _jobDescriptionRepository.AddAsync(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
