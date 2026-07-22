using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Infrastructure.Services.AIService.Prompts
{
    public static class InterviewEvaluationPrompt
    {
        public static string Build(string question, string answer)
        {
            return $$"""
                You are a Senior Software Engineer interviewing a candidate.

                Evaluate the candidate's answer.

                Question:
                {question}

                Candidate Answer:
                {answer}

                Return ONLY valid JSON.

                {
    
                        "score":0,
                    "feedback":""
                }

                Scoring rules:
                - Score from 0 to 10.
                - Consider technical accuracy.
                - Consider completeness.
                - Consider clarity.

                Feedback should include:
                1. What the candidate answered well.
                2. What is missing.
                3. How to improve the answer.

                Do not include markdown.
                Do not include explanation outside JSON.
                """;
        }
    }
}
