using AIResumeAnalyzer.Application.Common.Pagination;
using AIResumeAnalyzer.Application.Features.JobMatching.DTO;
using AIResumeAnalyzer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Interfaces.IRepository
{
    public interface IJobMatchRepository
    {
        Task<JobMatch?> GetDetailByIdAsync(Guid jobMatchId, Guid userId, CancellationToken cancellationToken);

        Task<int> GetCountAsync(Guid userId, CancellationToken cancellationToken);

        Task<double> GetAverageScoreAsync(Guid userId, CancellationToken cancellationToken);

        Task<double> GetBestScoreAsync(Guid userId, CancellationToken cancellationToken);

        Task<List<JobMatch>> GetRecentAsync(Guid userId, int take, CancellationToken cancellationToken);

        Task<List<JobMatch>>GetAllAsync(Guid userId, CancellationToken cancellationToken);

        Task<PagedResult<JobMatchHistoryItemResponse>> GetHistoryAsync(Guid userId, JobMatchFilterParams filter, CancellationToken cancellationToken);
    }
}
