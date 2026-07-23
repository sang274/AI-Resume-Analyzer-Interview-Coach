using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Resumes.Commands.Restore
{
    public class RestoreResumeCommandValidator : AbstractValidator<RestoreResumeCommand>
    {
        public RestoreResumeCommandValidator()
        {
            RuleFor(x => x.ResumeId).NotEmpty();
        }
    }
}
