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

        public async Task<InterviewSession?> GetDetailAsync(
            Guid sessionId,
            Guid userId,
            CancellationToken cancellationToken)
        {
            return await _context.InterviewSessions
                .AsNoTracking()
                .Include(x => x.Questions)
                .FirstOrDefaultAsync(x => x.Id == sessionId
                                    && x.UserId == userId,
                                    cancellationToken);
        }
    }
}
