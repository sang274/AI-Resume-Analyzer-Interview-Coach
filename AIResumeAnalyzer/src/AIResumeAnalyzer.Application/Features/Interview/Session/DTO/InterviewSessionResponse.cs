using AIResumeAnalyzer.Application.Features.Interview.Question.DTO;
using AIResumeAnalyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Session.DTO
{
    public class InterviewSessionResponse
    {
        public Guid Id { get; set; }

        public Guid ResumeId { get; set; }

        public InterviewType Type { get; set; }

        public double Score { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public List<InterviewQuestionResponse> Questions
        { get; set; }
            = [];
    }
}
