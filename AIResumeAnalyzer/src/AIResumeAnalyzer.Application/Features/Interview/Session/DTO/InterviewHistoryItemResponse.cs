using AIResumeAnalyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Session.DTO
{
    public class InterviewHistoryItemResponse
    {
        public Guid InterviewSessionId { get; set; }

        public Guid ResumeId { get; set; }

        public Guid? JobDescriptionId { get; set; }

        public string? JobTitle { get; set; }

        public string? CompanyName { get; set; }

        public InterviewType Type { get; set; }

        public double Score { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
