using AIResumeAnalyzer.Domain.Common;
using AIResumeAnalyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Domain.Entities
{
    public class InterviewSession : AuditableEntity
    {
        public Guid UserId { get; set; }

        public Guid ResumeId { get; set; }

        public Guid? JobDescriptionId { get; set; }

        public InterviewType Type { get; set; }

        public double Score { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public string? OverallFeedback { get; set; }

        public string? Strengths { get; set; }

        public string? Weaknesses { get; set; }

        public string? ImprovementSuggestions { get; set; }

        public bool IsCompleted { get; set; }

        public User User { get; set; } = null!;

        public Resume Resume { get; set; } = null!;

        public JobDescription? JobDescription { get; set; } = null!;

        public ICollection<InterviewQuestion> Questions { get; set; } = new List<InterviewQuestion>();
    }
}
