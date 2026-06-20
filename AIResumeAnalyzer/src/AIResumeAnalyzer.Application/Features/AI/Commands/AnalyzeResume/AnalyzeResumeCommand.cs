using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.AI.Commands.AnalyzeResume
{
    public record AnalyzeResumeCommand(Guid ResumeId) : IRequest<bool>;
}
