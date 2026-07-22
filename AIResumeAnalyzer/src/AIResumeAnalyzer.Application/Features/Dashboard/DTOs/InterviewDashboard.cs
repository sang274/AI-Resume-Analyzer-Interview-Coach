using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Dashboard.DTOs
{
    public class InterviewDashboard
    {
        public int Total { get; set; }

        public int Completed { get; set; }

        public double AverageInterviewScore { get; set; }

        public double BestInterviewScore { get; set; }
    }
}
