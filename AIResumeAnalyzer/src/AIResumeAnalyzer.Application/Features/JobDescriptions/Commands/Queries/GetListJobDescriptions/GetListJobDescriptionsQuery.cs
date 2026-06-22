using AIResumeAnalyzer.Application.Features.JobDescriptions.DTO;
using AIResumeAnalyzer.Application.Features.Resumes.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Queries.GetListJobDescriptions
{
    public record GetListJobDescriptionsQuery() : IRequest<List<JobDescriptionResponse>>;
}
