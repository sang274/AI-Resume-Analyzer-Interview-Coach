using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Dashboard.DTOs
{
    public class DashboardChartsResponse
    {
        public List<ChartItemResponse> AtsTrend { get; set; } = [];

        public List<ChartItemResponse> MatchTrend { get; set; } = [];

        public List<ChartItemResponse> InterviewTrend { get; set; } = [];
    }
}
