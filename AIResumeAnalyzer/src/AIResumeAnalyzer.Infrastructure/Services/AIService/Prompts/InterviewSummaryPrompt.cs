using AIResumeAnalyzer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Infrastructure.Services.AIService.Prompts
{
    public static class InterviewSummaryPrompt
    {
        public static string Build(IEnumerable<InterviewQuestion> questions)
        {
            var sb = new StringBuilder();

            foreach (var q in questions)
            {
                sb.AppendLine($"Question: {q.Question}");
                sb.AppendLine($"Answer: {q.Answer}");
                sb.AppendLine($"Score: {q.Score}");
                sb.AppendLine($"AI Feedback: {q.AIResponse}");
                sb.AppendLine();
            }

            return $$"""
                You are a Senior Engineering Manager.

                Analyze the following interview.

                Return ONLY JSON.

                {
    
                  "overallFeedback":"",
                  "strengths":"",
                  "weaknesses":"",
                  "improvementSuggestions":""
                }

                Interview:

                {{sb}}
                """;
        }
    }
}
