using AIResumeAnalyzer.Application.Common.Filtering;
using AIResumeAnalyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Resumes.DTOs
{
    public class ResumeFilterParams : FilterParams
    {
        public ResumeStatus? Status { get; set; }

        public double? MinATSScore { get; set; }

        public double? MaxATSScore { get; set; }
    }
}
