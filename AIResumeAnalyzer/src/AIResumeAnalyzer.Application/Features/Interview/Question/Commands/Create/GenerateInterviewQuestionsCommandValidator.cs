using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Question.Commands.Create
{
    public class GenerateInterviewQuestionsCommandValidator : AbstractValidator<GenerateInterviewQuestionsCommand>
    {
        public GenerateInterviewQuestionsCommandValidator()
        {
            RuleFor(x => x.ResumeId).NotEmpty();

            RuleFor(x => x.JobDescriptionId).NotEmpty();
        }
    }
}
