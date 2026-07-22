using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Dashboard.DTOs
{
    public class DashboardResponse
    {
        public ResumeDashboard Resume { get; set; } = new();

        public JobMatchDashboard JobMatch { get; set; } = new();

        public InterviewDashboard Interview { get; set; } = new();
    }
}
