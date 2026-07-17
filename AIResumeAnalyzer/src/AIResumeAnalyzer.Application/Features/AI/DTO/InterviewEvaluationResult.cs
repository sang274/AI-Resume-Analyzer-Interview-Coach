using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.AI.DTO
{
    public class InterviewEvaluationResult
    {
        public double Score { get; set; }

        public string Feedback { get; set; } = string.Empty;
    }
}
