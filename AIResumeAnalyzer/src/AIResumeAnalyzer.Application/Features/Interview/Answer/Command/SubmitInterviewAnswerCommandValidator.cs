using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Answer.Command
{
    public class SubmitInterviewAnswerCommandValidator : AbstractValidator<SubmitInterviewAnswerCommand>
    {
        public SubmitInterviewAnswerCommandValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty();

            RuleFor(x => x.Answer)
                .NotEmpty()
                .MaximumLength(5000);
        }
    }
}
