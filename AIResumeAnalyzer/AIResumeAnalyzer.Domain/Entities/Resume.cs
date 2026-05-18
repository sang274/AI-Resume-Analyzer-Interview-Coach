using AIResumeAnalyzer.Domain.Common;
using AIResumeAnalyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Domain.Entities
{
    public class Resume : AuditableEntity
    {
        public Guid UserId { get; set; }

        public string FileName { get; set; } = null!;

        public string FileUrl { get; set; } = null!;

        public string ParsedText { get; set; } = null!;

        public double ATSScore { get; set; }

        public ResumeStatus Status { get; set; }

        public User User { get; set; } = null!;

        public ResumeAnalysis? ResumeAnalysis { get; set; }

        public ICollection<JobMatch> JobMatches { get; set; }
            = new List<JobMatch>();
    }
}
