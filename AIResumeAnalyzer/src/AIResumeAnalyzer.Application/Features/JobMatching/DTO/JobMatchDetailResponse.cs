using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobMatching.DTO
{
    public class JobMatchDetailResponse
    {
        public Guid Id { get; set; }

        public Guid ResumeId { get; set; }

        public Guid JobDescriptionId { get; set; }

        public double MatchScore { get; set; }

        public string MissingKeywords { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
