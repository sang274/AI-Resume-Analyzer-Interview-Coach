using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Domain.Entities;
using AIResumeAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Infrastructure.Repositories
{
    public class InterviewSessionRepository : IInterviewSessionRepository
    {
        private readonly AppDbContext _context;

        public InterviewSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InterviewSession>> GetMySessionsAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.InterviewSessions
                .AsNoTracking()
                .Include(x => x.JobDescription)
                .Include(x => x.Questions)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<InterviewSession?> GetDetailAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken)
        {
            return await _context.InterviewSessions
                .AsNoTracking()
                .Include(x => x.JobDescription)
                .Include(x => x.Questions)
                .FirstOrDefaultAsync(x => x.Id == sessionId
                                    && x.UserId == userId,
                                    cancellationToken);
        }

        public void Update(InterviewSession session)
        {
            _context.InterviewSessions.Update(session);
        }

        public async Task<InterviewSession?> GetForFinishAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken)
        {
            return await _context.InterviewSessions
                .Include(x => x.Questions)
                .FirstOrDefaultAsync(
                    x => x.Id == sessionId &&
                         x.UserId == userId,
                    cancellationToken);
        }

        public async Task<int> GetCountAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.InterviewSessions.CountAsync(x => x.UserId == userId, cancellationToken);
        }

        public async Task<int> GetCompletedCountAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.InterviewSessions.CountAsync(x => x.UserId == userId && x.IsCompleted, cancellationToken);
        }

        public async Task<double> GetAverageScoreAsync(Guid userId, CancellationToken cancellationToken)
        {
            var average = await _context.InterviewSessions
                .Where(x => x.UserId == userId && x.IsCompleted)
                .AverageAsync(x => (double?)x.Score, cancellationToken);

            return average ?? 0;
        }

        public async Task<double> GetBestScoreAsync(Guid userId, CancellationToken cancellationToken)
        {
            var bestScore = await _context.InterviewSessions
                .Where(x => x.UserId == userId && x.IsCompleted)
                .MaxAsync(x => (double?)x.Score, cancellationToken);

            return bestScore ?? 0;
        }

        public async Task<List<InterviewSession>> GetRecentAsync(Guid userId, int take, CancellationToken cancellationToken)
        {
            return await _context.InterviewSessions
                .Include(x => x.JobDescription)
                .Where(x => x.Resume.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Take(take)
                .ToListAsync(cancellationToken);
        }
    }
}
