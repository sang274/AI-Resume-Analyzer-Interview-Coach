using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobMatching.DTO
{
    public class JobMatchResult
    {
        public double MatchScore { get; set; }

        public string MissingKeywords { get; set; }  = string.Empty;

        public string MatchedKeywords { get; set; }

        public string Suggestions { get; set; }
    }
}
