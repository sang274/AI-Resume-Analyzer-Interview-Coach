using AIResumeAnalyzer.Application.Common.Extensions;
using AIResumeAnalyzer.Application.Common.Pagination;
using AIResumeAnalyzer.Application.Features.Interview.Session.DTO;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Domain.Entities;
using AIResumeAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<List<InterviewSession>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.InterviewSessions
                .Include(x => x.JobDescription)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<PagedResult<InterviewHistoryItemResponse>> GetHistoryAsync(Guid userId, InterviewFilterParams filter, CancellationToken cancellationToken)
        {
            IQueryable<InterviewSession> query = _context.InterviewSessions
                .AsNoTracking()
                .Include(x => x.JobDescription)
                .Where(x => x.UserId == userId);

            // Keyword
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                var keyword = filter.Keyword.Trim().ToLower();

                query = query.Where(x => x.JobDescription != null &&
                    (
                        x.JobDescription.Title.ToLower().Contains(keyword) || x.JobDescription.CompanyName.ToLower().Contains(keyword)
                    ));
            }

            // Interview Type
            if (filter.Type.HasValue)
            {
                query = query.Where(x => x.Type == filter.Type.Value);
            }

            // Completed
            if (filter.IsCompleted.HasValue)
            {
                query = query.Where(x => x.IsCompleted == filter.IsCompleted.Value);
            }

            // Score
            if (filter.MinScore.HasValue)
            {
                query = query.Where(x => x.Score >= filter.MinScore.Value);
            }

            if (filter.MaxScore.HasValue)
            {
                query = query.Where(x => x.Score <= filter.MaxScore.Value);
            }

            // Sorting
            query = query.ApplySorting(filter.Sort, SortColumns, x => x.CreatedAt);

            // Projection
            var projected = query.Select(x => new InterviewHistoryItemResponse
            {
                InterviewSessionId = x.Id,

                ResumeId = x.ResumeId,

                JobDescriptionId = x.JobDescriptionId,

                JobTitle = x.JobDescription != null ? x.JobDescription.Title : null,

                CompanyName = x.JobDescription != null ? x.JobDescription.CompanyName : null,

                Type = x.Type,

                Score = x.Score,

                IsCompleted = x.IsCompleted,

                StartedAt = x.StartedAt,

                FinishedAt = x.FinishedAt,

                CreatedAt = x.CreatedAt
            });

            // Pagination
            return await projected.ToPagedResultAsync(filter, cancellationToken);
        }

        private static readonly Dictionary<string, Expression<Func<InterviewSession, object>>> SortColumns = new()
        {
            ["score"] = x => x.Score,

            ["type"] = x => x.Type,

            ["startedat"] = x => x.StartedAt,

            ["finishedat"] = x => x.FinishedAt ?? DateTime.MaxValue,

            ["createdat"] = x => x.CreatedAt
        };
    }
}
