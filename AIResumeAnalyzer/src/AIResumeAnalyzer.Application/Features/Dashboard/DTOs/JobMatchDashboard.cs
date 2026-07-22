using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Dashboard.DTOs
{
    public class JobMatchDashboard
    {
        public int Total { get; set; }

        public double AverageMatchScore { get; set; }

        public double BestMatchScore { get; set; }
    }
}
