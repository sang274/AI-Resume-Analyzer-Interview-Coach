using AIResumeAnalyzer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Interfaces.IRepository
{
    public interface IResumeRepository
    {
        Task<int> GetCountAsync(Guid userId, CancellationToken cancellationToken);

        Task<double> GetAverageATSScoreAsync(Guid userId, CancellationToken cancellationToken);

        Task<double> GetBestATSScoreAsync(Guid userId, CancellationToken cancellationToken);

        Task<List<Resume>>GetRecentAsync(Guid userId, int take, CancellationToken cancellationToken);

        Task<List<Resume>>GetAllAsync(Guid userId, CancellationToken cancellationToken);
    }
}
