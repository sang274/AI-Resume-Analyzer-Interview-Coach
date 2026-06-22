using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Create
{
    public class CreateJobDescriptionValidator : AbstractValidator<CreateJobDescriptionCommand>
    {
        public CreateJobDescriptionValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.CompanyName)
                .NotEmpty();

            RuleFor(x => x.Content)
                .NotEmpty();
        }
    }
}
