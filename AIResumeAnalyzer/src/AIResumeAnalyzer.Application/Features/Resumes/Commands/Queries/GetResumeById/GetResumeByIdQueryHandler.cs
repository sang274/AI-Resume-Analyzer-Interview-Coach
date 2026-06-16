using AIResumeAnalyzer.Application.Common.Exceptions;
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

namespace AIResumeAnalyzer.Application.Features.Resumes.Commands.Queries.GetResumeById
{
    public class GetResumeByIdQueryHandler : IRequestHandler<GetResumeByIdQuery, ResumeDetailResponse>
    {
        private readonly IGenericRepository<Resume> _resumeRepository;

        private readonly ICurrentUserService _currentUserService;

        public GetResumeByIdQueryHandler(
            IGenericRepository<Resume> resumeRepository,
            ICurrentUserService currentUserService)
        {
            _resumeRepository = resumeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ResumeDetailResponse> Handle(GetResumeByIdQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.UserId.HasValue)
            {
                throw new UnauthorizedAccessException();
            }

            var resume = await _resumeRepository.FirstOrDefaultAsync(
                    x => x.Id == request.ResumeId
                      && x.UserId == _currentUserService.UserId.Value);

            if (resume is null)
            {
                throw new NotFoundException("Resume not found.");
            }

            return new ResumeDetailResponse
            {
                Id = resume.Id,
                FileName = resume.FileName,
                FileUrl = resume.FileUrl,
                ParsedText = resume.ParsedText,
                ATSScore = resume.ATSScore,
                Status = resume.Status,
                CreatedAt = resume.CreatedAt
            };
        }
    }
}
