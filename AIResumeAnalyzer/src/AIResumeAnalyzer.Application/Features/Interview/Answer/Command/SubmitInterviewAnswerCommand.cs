using AIResumeAnalyzer.Application.Features.Interview.Answer.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Answer.Command
{
    public record SubmitInterviewAnswerCommand(Guid QuestionId, string Answer) : IRequest<SubmitInterviewAnswerResponse>;
}
