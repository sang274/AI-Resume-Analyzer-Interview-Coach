using AIResumeAnalyzer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Domain.Entities
{
    public class ResumeAnalysis : AuditableEntity
    {
        public Guid ResumeId { get; set; }

        public string Strengths { get; set; } = null!;

        public string Weaknesses { get; set; } = null!;

        public string Suggestions { get; set; } = null!;

        public string MissingSkills { get; set; } = null!;

        public Resume Resume { get; set; } = null!;
    }
}
