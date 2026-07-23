using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Dashboard.DTOs
{
    public class ChartItemResponse
    {
        public DateTime Date { get; set; }

        public string Label { get; set; } = string.Empty;

        public double Value { get; set; }
    }
}
