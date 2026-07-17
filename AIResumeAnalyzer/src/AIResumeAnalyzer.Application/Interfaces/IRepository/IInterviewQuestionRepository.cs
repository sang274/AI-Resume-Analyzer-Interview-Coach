using AIResumeAnalyzer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Interfaces.IRepository
{
    public interface IInterviewQuestionRepository
    {
        Task<InterviewQuestion?> GetDetailAsync(
            Guid questionId,
            Guid userId,
            CancellationToken cancellationToken);

        void Update(InterviewQuestion question);
    }
}
