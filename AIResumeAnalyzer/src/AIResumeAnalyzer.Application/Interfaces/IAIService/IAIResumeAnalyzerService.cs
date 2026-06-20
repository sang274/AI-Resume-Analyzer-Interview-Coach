using AIResumeAnalyzer.Application.Features.AI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Interfaces.IAIService
{
    public interface IAIResumeAnalyzerService
    {
        Task<ResumeAnalysisResult> AnalyzeAsync(string resumeText, CancellationToken cancellationToken);
    }
}
