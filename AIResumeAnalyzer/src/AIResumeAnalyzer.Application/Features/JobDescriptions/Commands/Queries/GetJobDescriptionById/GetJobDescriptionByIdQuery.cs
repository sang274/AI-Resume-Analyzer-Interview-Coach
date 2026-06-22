using AIResumeAnalyzer.Application.Features.JobDescriptions.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Queries.GetJobDescriptionById
{
    public record GetJobDescriptionByIdQuery(Guid JobDescriptionId) : IRequest<JobDescriptionResponseDetail>;
}
