using AIResumeAnalyzer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Interfaces.IRepository
{
    public interface IInterviewSessionRepository
    {
        Task<InterviewSession?> GetDetailAsync(
            Guid sessionId,
            Guid userId,
            CancellationToken cancellationToken);
    }
}
