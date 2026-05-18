using AIResumeAnalyzer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Domain.Entities
{
    public class JobMatch : AuditableEntity
    {
        public Guid ResumeId { get; set; }

        public Guid JobDescriptionId { get; set; }

        public double MatchScore { get; set; }

        public string MissingKeywords { get; set; } = null!;

        public Resume Resume { get; set; } = null!;

        public JobDescription JobDescription { get; set; } = null!;
    }
}
