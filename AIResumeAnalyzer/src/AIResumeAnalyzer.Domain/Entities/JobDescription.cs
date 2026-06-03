using AIResumeAnalyzer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Domain.Entities
{
    public class JobDescription : AuditableEntity
    {
        public string Title { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string Content { get; set; } = null!;

        public ICollection<JobMatch> JobMatches { get; set; }
            = new List<JobMatch>();
    }
}
