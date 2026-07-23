using AIResumeAnalyzer.Application.Common.Extensions;
using AIResumeAnalyzer.Application.Common.Pagination;
using AIResumeAnalyzer.Application.Features.JobMatching.DTO;
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
    public class JobMatchRepository : IJobMatchRepository
    {
        private readonly AppDbContext _context;

        public JobMatchRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<JobMatch?> GetDetailByIdAsync(Guid jobMatchId, Guid userId, CancellationToken cancellationToken)
        {
            return await _context.JobMatches
                .AsNoTracking()
                .Include(x => x.JobDescription)
                .Include(x => x.Resume)
                .FirstOrDefaultAsync(x => x.Id == jobMatchId && x.Resume.UserId == userId, cancellationToken);
        }

        public async Task<int> GetCountAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.JobMatches
                .Where(x => x.Resume.UserId == userId)
                .CountAsync(cancellationToken);
        }

        public async Task<double> GetAverageScoreAsync(Guid userId, CancellationToken cancellationToken)
        {
            var average = await _context.JobMatches
                .Where(x => x.Resume.UserId == userId)
                .AverageAsync(x => (double?)x.MatchScore, cancellationToken);

            return average ?? 0;
        }

        public async Task<double> GetBestScoreAsync(Guid userId, CancellationToken cancellationToken)
        {
            var bestScore = await _context.JobMatches
                .Where(x => x.Resume.UserId == userId)
                .MaxAsync(x => (double?)x.MatchScore, cancellationToken);

            return bestScore ?? 0;
        }

        public async Task<List<JobMatch>> GetRecentAsync(Guid userId, int take, CancellationToken cancellationToken)
        {
            return await _context.JobMatches
                .Include(x => x.JobDescription)
                .Include(x => x.Resume)
                .Where(x => x.Resume.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<JobMatch>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.JobMatches
                .Include(x => x.JobDescription)
                .Include(x => x.Resume)
                .Where(x => x.Resume.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<PagedResult<JobMatchHistoryItemResponse>> GetHistoryAsync(Guid userId, JobMatchFilterParams filter, CancellationToken cancellationToken)
        {
            IQueryable<JobMatch> query = _context.JobMatches
                .AsNoTracking()
                .Include(x => x.JobDescription)
                .Include(x => x.Resume)
                .Where(x => x.Resume.UserId == userId);

            // Keyword
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                var keyword = filter.Keyword.Trim().ToLower();

                query = query.Where(x => x.JobDescription.Title.ToLower().Contains(keyword) || x.JobDescription.CompanyName.ToLower().Contains(keyword));
            }

            // Company
            if (!string.IsNullOrWhiteSpace(filter.CompanyName))
            {
                var company = filter.CompanyName.Trim().ToLower();

                query = query.Where(x => x.JobDescription.CompanyName.ToLower().Contains(company));
            }

            // Job Title
            if (!string.IsNullOrWhiteSpace(filter.JobTitle))
            {
                var title = filter.JobTitle.Trim().ToLower();

                query = query.Where(x => x.JobDescription.Title.ToLower().Contains(title));
            }

            // Match Score
            if (filter.MinMatchScore.HasValue)
            {
                query = query.Where(x => x.MatchScore >= filter.MinMatchScore.Value);
            }

            if (filter.MaxMatchScore.HasValue)
            {
                query = query.Where(x => x.MatchScore <= filter.MaxMatchScore.Value);
            }

            // Sorting
            query = query.ApplySorting(filter.Sort, SortColumns, x => x.CreatedAt);

            // Projection
            var projected = query.Select(x => new JobMatchHistoryItemResponse
            {
                JobMatchId = x.Id,

                ResumeId = x.ResumeId,

                JobDescriptionId = x.JobDescriptionId,

                CompanyName = x.JobDescription.CompanyName,

                JobTitle = x.JobDescription.Title,

                MatchScore = x.MatchScore,

                CreatedAt = x.CreatedAt
            });

            return await projected.ToPagedResultAsync(
                filter,
                cancellationToken);
        }

        private static readonly Dictionary<string, Expression<Func<JobMatch, object>>> SortColumns = new()
        {
            ["company"] = x => x.JobDescription.CompanyName,

            ["title"] = x => x.JobDescription.Title,

            ["matchscore"] = x => x.MatchScore,

            ["createdat"] = x => x.CreatedAt
        };
    }
}
