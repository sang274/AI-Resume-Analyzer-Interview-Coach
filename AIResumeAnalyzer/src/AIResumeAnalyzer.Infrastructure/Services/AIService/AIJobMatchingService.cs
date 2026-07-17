using AIResumeAnalyzer.Application.Common.Settings;
using AIResumeAnalyzer.Application.Features.AI.DTO;
using AIResumeAnalyzer.Application.Features.JobMatching.DTO;
using AIResumeAnalyzer.Application.Interfaces.IJobService;
using AIResumeAnalyzer.Infrastructure.Services.AIService.Prompts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Infrastructure.Services.AIService
{
    public class AIJobMatchingService : IJobMatchingService
    {
        private readonly IGeminiClient _aiClient;

        public AIJobMatchingService(IGeminiClient aiClient)
        {
            _aiClient = aiClient;
        }

        public async Task<JobMatchResult> AnalyzeAsync(
                string resumeText,
                string jobDescriptionText,
                CancellationToken cancellationToken)
        {
            var prompt = JobMatchPrompt.Build(resumeText, jobDescriptionText);

            return await _aiClient.SendPromptAsync<JobMatchResult>(prompt, cancellationToken);
        }
    }
}
