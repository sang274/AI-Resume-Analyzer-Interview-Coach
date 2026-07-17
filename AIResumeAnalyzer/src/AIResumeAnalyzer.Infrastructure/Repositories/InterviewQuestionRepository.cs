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
    public class InterviewQuestionRepository : IInterviewQuestionRepository
    {
        private readonly AppDbContext _context;

        public InterviewQuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<InterviewQuestion?> GetDetailAsync(
            Guid questionId,
            Guid userId,
            CancellationToken cancellationToken)
        {
            return await _context.InterviewQuestions
                .Include(x => x.InterviewSession)
                .FirstOrDefaultAsync(x => x.Id == questionId && x.InterviewSession.UserId == userId, cancellationToken);
        }

        public void Update(InterviewQuestion question)
        {
            _context.InterviewQuestions.Update(question);
        }
    }
}
