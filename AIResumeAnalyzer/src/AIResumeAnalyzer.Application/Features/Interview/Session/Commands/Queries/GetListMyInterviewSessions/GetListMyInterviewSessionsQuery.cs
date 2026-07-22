using AIResumeAnalyzer.Application.Features.Interview.Session.DTO;
using AIResumeAnalyzer.Application.Features.JobDescriptions.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Session.Commands.Queries.GetInterviewSessions
{
    public record GetListMyInterviewSessionsQuery() : IRequest<List<InterviewSessionListItemResponse>>;
}
