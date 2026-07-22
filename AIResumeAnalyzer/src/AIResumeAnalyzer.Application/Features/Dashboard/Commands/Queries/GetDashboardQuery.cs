using AIResumeAnalyzer.Application.Features.Dashboard.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Dashboard.Commands.Queries
{
    public record GetDashboardQuery : IRequest<DashboardResponse>;
}
