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
        Task<List<InterviewSession>>GetMySessionsAsync(Guid userId, CancellationToken cancellationToken);

        Task<InterviewSession?> GetDetailAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken);

        Task<InterviewSession?> GetForFinishAsync(Guid sessionId,Guid userId,CancellationToken cancellationToken);

        void Update(InterviewSession session);

        Task<int> GetCountAsync(Guid userId, CancellationToken cancellationToken);

        Task<int> GetCompletedCountAsync(Guid userId, CancellationToken cancellationToken);

        Task<double> GetAverageScoreAsync(Guid userId, CancellationToken cancellationToken);

        Task<double> GetBestScoreAsync(Guid userId, CancellationToken cancellationToken);

        Task<List<InterviewSession>> GetRecentAsync(Guid userId, int take, CancellationToken cancellationToken);
    }
}
