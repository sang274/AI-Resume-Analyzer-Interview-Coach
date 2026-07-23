using AIResumeAnalyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Resumes.DTOs
{
    public class ResumeHistoryItemResponse
    {
        public Guid ResumeId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public double ATSScore { get; set; }

        public ResumeStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
