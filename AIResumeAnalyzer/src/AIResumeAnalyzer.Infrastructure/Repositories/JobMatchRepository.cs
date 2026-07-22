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
    }
}
