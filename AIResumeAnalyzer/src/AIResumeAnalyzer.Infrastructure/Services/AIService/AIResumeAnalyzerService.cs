using AIResumeAnalyzer.Application.Common.Settings;
using AIResumeAnalyzer.Application.Features.AI.DTO;
using AIResumeAnalyzer.Application.Interfaces.IAIService;
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
    public class AIResumeAnalyzerService : IAIResumeAnalyzerService
    {
        private readonly IGeminiClient _aiClient;

        public AIResumeAnalyzerService(IGeminiClient aiClient)
        {
            _aiClient = aiClient;
        }

        public async Task<ResumeAnalysisResult> AnalyzeAsync(
            string resumeText,
            CancellationToken cancellationToken)
        {
            var prompt = ResumeAnalysisPrompt.Build(resumeText);

            return await _aiClient.SendPromptAsync<ResumeAnalysisResult>(prompt, cancellationToken);
        }
    }
}
