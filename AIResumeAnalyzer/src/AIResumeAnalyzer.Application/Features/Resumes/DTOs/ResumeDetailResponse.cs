using AIResumeAnalyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Resumes.DTOs
{
    public class ResumeDetailResponse
    {
        public Guid Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FileUrl { get; set; } = string.Empty;

        public string ParsedText { get; set; } = string.Empty;

        public double ATSScore { get; set; }

        public ResumeStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
