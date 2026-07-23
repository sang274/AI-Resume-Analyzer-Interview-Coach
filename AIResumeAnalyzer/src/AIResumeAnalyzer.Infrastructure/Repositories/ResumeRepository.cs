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
    }
}
