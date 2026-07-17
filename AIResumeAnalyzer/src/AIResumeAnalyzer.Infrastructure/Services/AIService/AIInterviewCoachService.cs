using AIResumeAnalyzer.Application.Common.Settings;
using AIResumeAnalyzer.Application.Features.AI.DTO;
using AIResumeAnalyzer.Application.Features.Interview.Question.DTO;
using AIResumeAnalyzer.Application.Interfaces.IInterviewService;
using AIResumeAnalyzer.Infrastructure.Services.AIService.Prompts;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace AIResumeAnalyzer.Infrastructure.Services.AIService
{
    public class AIInterviewCoachService : IInterviewCoachService
    {
        private readonly IGeminiClient _aiClient;

        public AIInterviewCoachService(IGeminiClient aiClient)
        {
            _aiClient = aiClient;
        }

        public async Task<InterviewQuestionGenerationResult> GenerateQuestionsAsync(
            string resumeText, string jobDescriptionText, CancellationToken cancellationToken)
        {
            var prompt = InterviewQuestionPrompt.Build(resumeText, jobDescriptionText);

            return await _aiClient.SendPromptAsync<InterviewQuestionGenerationResult>(prompt, cancellationToken);
        }

        public async Task<InterviewEvaluationResult> EvaluateAnswerAsync(
            string question, string answer, CancellationToken cancellationToken)
        {
            var prompt = InterviewEvaluationPrompt.Build(question, answer);

            return await _aiClient.SendPromptAsync<InterviewEvaluationResult>(prompt, cancellationToken);
        }
    }
}