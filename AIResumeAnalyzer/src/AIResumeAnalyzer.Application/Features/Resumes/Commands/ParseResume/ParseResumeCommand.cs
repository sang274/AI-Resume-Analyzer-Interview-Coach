using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Resumes.Commands.ParseResume
{
    public record ParseResumeCommand(Guid ResumeId) : IRequest<bool>;
}
