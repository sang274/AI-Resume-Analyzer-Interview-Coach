using AIResumeAnalyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Session.DTO
{
    public class InterviewSessionListItemResponse
    {
        public Guid Id { get; set; }

        public string JobTitle { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public InterviewType Type { get; set; }

        public double Score { get; set; }

        public int QuestionCount { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }
    }
}
