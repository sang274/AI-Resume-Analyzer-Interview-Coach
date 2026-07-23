using AIResumeAnalyzer.Application.Common.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobMatching.DTO
{
    public class JobMatchFilterParams : FilterParams
    {
        public string? CompanyName { get; set; }

        public string? JobTitle { get; set; }

        public double? MinMatchScore { get; set; }

        public double? MaxMatchScore { get; set; }
    }
}
