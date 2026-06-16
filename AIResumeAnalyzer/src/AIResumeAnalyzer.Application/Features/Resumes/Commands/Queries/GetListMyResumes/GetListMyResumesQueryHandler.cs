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

namespace AIResumeAnalyzer.Application.Features.Resumes.Commands.Queries.GetListMyResumes
{
    public class GetListMyResumesQueryHandler : IRequestHandler<GetListMyResumesQuery, List<ResumeResponse>>
    {
        private readonly IGenericRepository<Resume> _resumeRepository;

        private readonly ICurrentUserService _currentUserService;

        public GetListMyResumesQueryHandler(
            IGenericRepository<Resume> resumeRepository,
            ICurrentUserService currentUserService)
        {
            _resumeRepository = resumeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<ResumeResponse>> Handle(GetListMyResumesQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.UserId.HasValue)
            {
                throw new UnauthorizedAccessException();
            }

            var resumes = await _resumeRepository.WhereAsync(x => x.UserId == _currentUserService.UserId.Value);

            return resumes.OrderByDescending(x => x.CreatedAt)
                .Select(x => new ResumeResponse
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    FileUrl = x.FileUrl,
                    ATSScore = x.ATSScore,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt
                })
                .ToList();
        }
    }
}
