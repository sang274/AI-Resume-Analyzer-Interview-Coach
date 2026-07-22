using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Dashboard.DTOs
{
    public class ResumeDashboard
    {
        public int Total { get; set; }

        public double AverageATSScore { get; set; }

        public double BestATSScore { get; set; }
    }
}
