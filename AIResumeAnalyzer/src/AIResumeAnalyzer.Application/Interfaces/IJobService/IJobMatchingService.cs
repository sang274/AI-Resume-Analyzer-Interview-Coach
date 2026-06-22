using AIResumeAnalyzer.Application.Features.JobMatching.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Interfaces.IJobService
{
    public interface IJobMatchingService
    {
        Task<JobMatchResult> AnalyzeAsync(
            string resumeText,
            string jobDescriptionText,
            CancellationToken cancellationToken);
    }
}
