using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Question.Commands.Create
{
    public record GenerateInterviewQuestionsCommand(Guid ResumeId, Guid JobDescriptionId) : IRequest<Guid>;
}
