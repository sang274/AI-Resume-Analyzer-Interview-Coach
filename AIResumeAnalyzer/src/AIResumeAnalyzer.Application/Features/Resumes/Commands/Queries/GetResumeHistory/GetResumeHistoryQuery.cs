using AIResumeAnalyzer.Application.Common.Pagination;
using AIResumeAnalyzer.Application.Features.Resumes.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Resumes.Commands.Queries.GetResumeHistory
{
    public record GetResumeHistoryQuery(ResumeFilterParams Filter) : IRequest<PagedResult<ResumeHistoryItemResponse>>;
}
