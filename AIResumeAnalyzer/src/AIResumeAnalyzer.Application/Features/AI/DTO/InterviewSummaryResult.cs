using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.AI.DTO
{
    public class InterviewSummaryResult
    {
        public string OverallFeedback { get; set; } = string.Empty;

        public string Strengths { get; set; } = string.Empty;

        public string Weaknesses { get; set; } = string.Empty;

        public string ImprovementSuggestions { get; set; } = string.Empty;
    }
}
