using AIResumeAnalyzer.Application.Features.AI.DTO;
using AIResumeAnalyzer.Application.Features.Interview.Question.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Interfaces.IInterviewService
{
    public interface IInterviewCoachService
    {
        Task<InterviewQuestionGenerationResult>GenerateQuestionsAsync(string resumeText, string jobDescriptionText, CancellationToken cancellationToken);

        Task<InterviewEvaluationResult>EvaluateAnswerAsync(string question, string answer, CancellationToken cancellationToken);
    }
}
