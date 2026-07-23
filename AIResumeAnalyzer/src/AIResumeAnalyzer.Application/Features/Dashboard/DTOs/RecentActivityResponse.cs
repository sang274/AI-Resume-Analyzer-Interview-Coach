using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Dashboard.DTOs
{
    public class RecentActivityResponse
    {
        public ActivityType Type { get; set; }

        public Guid ReferenceId { get; set; }

        public string Title { get; set; }
            = string.Empty;

        public string Description { get; set; }
            = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
