using AIResumeAnalyzer.Application.Common.Pagination;
using AIResumeAnalyzer.Application.Features.JobMatching.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobMatching.Commands.Queries.GetJobMatchHistory
{
    public record GetJobMatchHistoryQuery(JobMatchFilterParams Filter) : IRequest<PagedResult<JobMatchHistoryItemResponse>>;
}
