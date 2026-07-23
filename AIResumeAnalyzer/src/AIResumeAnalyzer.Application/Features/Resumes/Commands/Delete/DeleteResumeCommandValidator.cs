using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Resumes.Commands.Delete
{
    public class DeleteResumeCommandValidator : AbstractValidator<DeleteResumeCommand>
    {
        public DeleteResumeCommandValidator()
        {
            RuleFor(x => x.ResumeId).NotEmpty();
        }
    }
}
