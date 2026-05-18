using AIResumeAnalyzer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Domain.Entities
{
    public class InterviewQuestion : AuditableEntity
    {
        public Guid InterviewSessionId { get; set; }

        public string Question { get; set; } = null!;

        public string? Answer { get; set; }

        public string? AIResponse { get; set; }

        public double Score { get; set; }

        public InterviewSession InterviewSession { get; set; } = null!;
    }
}
