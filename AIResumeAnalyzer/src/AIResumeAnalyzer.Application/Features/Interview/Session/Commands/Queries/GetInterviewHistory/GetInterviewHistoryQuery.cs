using AIResumeAnalyzer.Application.Common.Pagination;
using AIResumeAnalyzer.Application.Features.Interview.Session.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Session.Commands.Queries.GetInterviewHistory
{
    public record GetInterviewHistoryQuery(InterviewFilterParams Filter) : IRequest<PagedResult<InterviewHistoryItemResponse>>;
}
