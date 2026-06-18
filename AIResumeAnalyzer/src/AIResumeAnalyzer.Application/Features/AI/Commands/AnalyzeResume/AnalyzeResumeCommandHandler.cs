using AIResumeAnalyzer.Application.Common.Exceptions;
using AIResumeAnalyzer.Application.Exceptions;
using AIResumeAnalyzer.Application.Interfaces.IAIService;
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

namespace AIResumeAnalyzer.Application.Features.AI.Commands.AnalyzeResume
{
    public class AnalyzeResumeCommandHandler : IRequestHandler<AnalyzeResumeCommand, bool>
    {
        private readonly IGenericRepository<Resume> _resumeRepository;

        private readonly IGenericRepository<ResumeAnalysis> _resumeAnalysisRepository;

        private readonly IAIResumeAnalyzerService _aiResumeAnalyzerService;

        private readonly ICurrentUserService _currentUserService;

        private readonly IUnitOfWork _unitOfWork;

        public AnalyzeResumeCommandHandler(
            IGenericRepository<Resume> resumeRepository,
            IGenericRepository<ResumeAnalysis> resumeAnalysisRepository,
            IAIResumeAnalyzerService aiResumeAnalyzerService,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _resumeRepository = resumeRepository;
            _resumeAnalysisRepository = resumeAnalysisRepository;
            _aiResumeAnalyzerService = aiResumeAnalyzerService;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AnalyzeResumeCommand request, CancellationToken cancellationToken)
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

            if (string.IsNullOrWhiteSpace(resume.ParsedText))
            {
                throw new BadRequestException("Resume has not been parsed yet.");
            }

            var existingAnalysis = await _resumeAnalysisRepository.FirstOrDefaultAsync(x => x.ResumeId == resume.Id);


            var analysisResult = await _aiResumeAnalyzerService.AnalyzeAsync(resume.ParsedText, cancellationToken);

            resume.ATSScore = analysisResult.ATSScore;

            _resumeRepository.Update(resume);

            if (existingAnalysis is not null)
            {
                existingAnalysis.Strengths = analysisResult.Strengths;

                existingAnalysis.Weaknesses = analysisResult.Weaknesses;

                existingAnalysis.Suggestions = analysisResult.Suggestions;

                existingAnalysis.MissingSkills = analysisResult.MissingSkills;

                _resumeAnalysisRepository.Update(existingAnalysis);
            }
            else
            {
                var resumeAnalysis = new ResumeAnalysis
                {
                    ResumeId = resume.Id,

                    Strengths = analysisResult.Strengths,

                    Weaknesses = analysisResult.Weaknesses,

                    Suggestions = analysisResult.Suggestions,

                    MissingSkills = analysisResult.MissingSkills
                };

                await _resumeAnalysisRepository
                    .AddAsync(resumeAnalysis);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
