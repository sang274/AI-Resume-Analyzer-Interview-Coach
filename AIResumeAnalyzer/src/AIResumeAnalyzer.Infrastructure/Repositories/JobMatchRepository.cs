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
    }
}
