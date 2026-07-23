using AIResumeAnalyzer.Application.Common.Extensions;
using AIResumeAnalyzer.Application.Common.Pagination;
using AIResumeAnalyzer.Application.Features.Resumes.DTOs;
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
    public class ResumeRepository : IResumeRepository
    {
        private readonly AppDbContext _context;

        public ResumeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetCountAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Resumes.CountAsync(x => x.UserId == userId, cancellationToken);
        }

        public async Task<double> GetAverageATSScoreAsync(Guid userId, CancellationToken cancellationToken)
        {
            var average = await _context.Resumes
                .Where(x => x.UserId == userId)
                .AverageAsync(x => (double?)x.ATSScore, cancellationToken);

            return average ?? 0;
        }

        public async Task<double> GetBestATSScoreAsync(Guid userId, CancellationToken cancellationToken)
        {
            var bestScore = await _context.Resumes
                .Where(x => x.UserId == userId)
                .MaxAsync(x => (double?)x.ATSScore, cancellationToken);

            return bestScore ?? 0;
        }

        public async Task<List<Resume>> GetRecentAsync(Guid userId, int take, CancellationToken cancellationToken)
        {
            return await _context.Resumes
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Resume>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Resumes
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<PagedResult<ResumeHistoryItemResponse>> GetHistoryAsync(Guid userId, ResumeFilterParams filter, CancellationToken cancellationToken)
        {
            IQueryable<Resume> query = _context.Resumes
                .AsNoTracking()
                .Where(x => x.UserId == userId);

            // Search
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                var keyword = filter.Keyword.Trim().ToLower();

                query = query.Where(x => x.FileName.ToLower().Contains(keyword));
            }

            // Status
            if (filter.Status.HasValue)
            {
                query = query.Where(x => x.Status == filter.Status.Value);
            }

            // ATS Score
            if (filter.MinATSScore.HasValue)
            {
                query = query.Where(x => x.ATSScore >= filter.MinATSScore.Value);
            }

            if (filter.MaxATSScore.HasValue)
            {
                query = query.Where(x => x.ATSScore <= filter.MaxATSScore.Value);
            }

            // Sorting
            query = query.ApplySorting(
                filter.Sort,
                SortColumns,
                x => x.CreatedAt);

            // Projection
            var projected = query.Select(x => new ResumeHistoryItemResponse
            {
                ResumeId = x.Id,
                FileName = x.FileName,
                ATSScore = x.ATSScore,
                Status = x.Status,
                CreatedAt = x.CreatedAt
            });

            // Pagination
            return await projected.ToPagedResultAsync(
                filter,
                cancellationToken);
        }

        private static readonly Dictionary<string, Expression<Func<Resume, object>>> SortColumns = new()
        {
            ["filename"] = x => x.FileName,
            ["atsscore"] = x => x.ATSScore,
            ["status"] = x => x.Status,
            ["createdat"] = x => x.CreatedAt
        };
    }
}
