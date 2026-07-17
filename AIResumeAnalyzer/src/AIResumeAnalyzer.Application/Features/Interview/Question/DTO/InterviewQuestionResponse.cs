using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Question.DTO
{
    public class InterviewQuestionResponse
    {
        public Guid Id { get; set; }

        public string Question { get; set; } = string.Empty;

        public string? Answer { get; set; }

        public string? AIResponse { get; set; }

        public double Score { get; set; }
    }
}
