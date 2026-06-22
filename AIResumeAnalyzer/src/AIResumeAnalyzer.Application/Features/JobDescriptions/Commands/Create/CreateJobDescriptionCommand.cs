using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Create
{
    public record CreateJobDescriptionCommand(
        string Title,
        string CompanyName,
        string Content
    ) : IRequest<Guid>;
}
