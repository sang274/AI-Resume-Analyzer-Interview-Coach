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
    }
}
