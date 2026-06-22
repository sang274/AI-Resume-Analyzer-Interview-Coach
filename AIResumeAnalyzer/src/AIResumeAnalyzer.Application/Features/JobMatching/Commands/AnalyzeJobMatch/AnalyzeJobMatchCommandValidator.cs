
using FluentValidation;

namespace AIResumeAnalyzer.Application.Features.JobMatching.Commands.AnalyzeJobMatch
{
    public class AnalyzeJobMatchCommandValidator : AbstractValidator<AnalyzeJobMatchCommand>
    {
        public AnalyzeJobMatchCommandValidator()
        {
            RuleFor(x => x.ResumeId).NotEmpty();

            RuleFor(x => x.JobDescriptionId).NotEmpty();
        }
    }
}
