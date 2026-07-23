using AIResumeAnalyzer.Application.Common.Filtering;
using AIResumeAnalyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Session.DTO
{
    public class InterviewFilterParams : FilterParams
    {
        public InterviewType? Type { get; set; }

        public bool? IsCompleted { get; set; }

        public double? MinScore { get; set; }

        public double? MaxScore { get; set; }
    }
}
