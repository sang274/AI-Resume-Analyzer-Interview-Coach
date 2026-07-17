using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Answer.DTO
{
    public class SubmitInterviewAnswerResponse
    {
        public Guid QuestionId { get; set; }

        public double Score { get; set; }

        public string Feedback { get; set; } = string.Empty;
    }
}
