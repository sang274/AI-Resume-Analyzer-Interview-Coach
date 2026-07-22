using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Session.DTO
{
    public class InterviewSummaryResponse
    {
        public double AverageScore { get; set; }

        public string OverallFeedback { get; set; } = string.Empty;

        public string Strengths { get; set; } = string.Empty;

        public string Weaknesses { get; set; } = string.Empty;

        public string ImprovementSuggestions { get; set; } = string.Empty;
    }
}
