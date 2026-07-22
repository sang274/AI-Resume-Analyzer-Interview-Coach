using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Infrastructure.Services.AIService.Prompts
{
    public static class JobMatchPrompt
    {
        public static string Build(string resume, string jobDescription)
        {
            return $$"""
            You are an ATS Matching Expert.

            Compare Resume and Job Description.

            Return ONLY valid JSON.

            {
    
                "matchScore": 0,
                "matchedKeywords": "",
                "missingKeywords": "",
                "suggestions": ""
            }

            Rules:
            - Match score must be 0-100.
            - MatchedKeywords = skills found in both resume and JD.
            - MissingKeywords = skills required by JD but missing in resume.
            - Suggestions = specific improvements to increase matching score.

            Resume:
            {resume}

            Job Description:
            {jobDescription}
            """;
        }
    }
}
