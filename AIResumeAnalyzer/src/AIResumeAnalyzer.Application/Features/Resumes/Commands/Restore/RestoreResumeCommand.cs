using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Resumes.Commands.Restore
{
    public record RestoreResumeCommand(Guid ResumeId) : IRequest;
}
