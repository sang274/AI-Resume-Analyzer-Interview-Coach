using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Infrastructure.Services.AIService.Prompts
{
    public static class JobMatchPrompt
    {
        public static string Build(
            string resume,
            string jobDescription)
        {
            return $$"""
            You are an ATS Matching Engine.

            Compare the resume and job description.

            Return ONLY valid JSON.

            {
    
                  "matchScore": 0,
              "missingKeywords": ""
            }

            Rules:
            - matchScore must be between 0 and 100.
            - missingKeywords should contain missing skills separated by commas.

            Resume:
            {resume}

            Job Description:
            {jobDescription}
            """;
        }
    }
}
