using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.AI.Commands.AnalyzeResume
{
    public class AnalyzeResumeCommandValidator : AbstractValidator<AnalyzeResumeCommand>
    {
        public AnalyzeResumeCommandValidator()
        {
            RuleFor(x => x.ResumeId).NotEmpty();
        }
    }
}
