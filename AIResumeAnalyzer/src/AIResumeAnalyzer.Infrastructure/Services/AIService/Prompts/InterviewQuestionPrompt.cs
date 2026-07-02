using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Infrastructure.Services.AIService.Prompts
{
    public static class InterviewQuestionPrompt
    {
        public static string Build(string resume, string jobDescription)
        {
            return $$"""
            You are a Senior Technical Interviewer.

            Based on the Resume and Job Description below,
            generate 10 interview questions.

            Requirements:
            - Questions must be relevant to the job.
            - Mix technical and behavioral questions.
            - Questions should assess the candidate's experience.
            - Return ONLY valid JSON.

            {
    
                    "questions": [
                    "Question 1",
                    "Question 2"
                ]
            }

            Resume:
            {resume}

            Job Description:
            {jobDescription}
            """;
        }
    }
}
