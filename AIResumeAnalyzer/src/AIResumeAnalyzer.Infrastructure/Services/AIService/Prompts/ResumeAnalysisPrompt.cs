using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Infrastructure.Services.AIService.Prompts
{
    public static class ResumeAnalysisPrompt
    {
        public static string Build(string resumeText)
        {
            return $$"""
                You are a professional ATS Resume Reviewer.

                Analyze the following resume.

                Return ONLY a valid raw JSON object. Do not wrap the response in markdown blocks like ```json ... ```.

                {
                  "atsScore": 0,
                  "strengths": "",
                  "weaknesses": "",
                  "suggestions": "",
                  "missingSkills": ""
                }

                Rules:
                - ATS Score must be between 0 and 100.
                - Strengths should summarize key strengths.
                - Weaknesses should identify shortcomings.
                - Suggestions should provide actionable improvements.
                - MissingSkills should list important missing skills separated by commas.

                Resume:

                {{resumeText}}
                """;
        }
    }
}
