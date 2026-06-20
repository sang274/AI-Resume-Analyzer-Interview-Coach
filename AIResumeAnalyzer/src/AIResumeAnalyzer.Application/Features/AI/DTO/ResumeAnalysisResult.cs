using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.AI.DTO
{
    public class ResumeAnalysisResult
    {
        public double ATSScore { get; set; }

        public string Strengths { get; set; } = string.Empty;

        public string Weaknesses { get; set; } = string.Empty;

        public string Suggestions { get; set; } = string.Empty;

        public string MissingSkills { get; set; } = string.Empty;
    }
}
