using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Resumes.Commands.UploadResume
{
    public class UploadResumeCommandValidator : AbstractValidator<UploadResumeCommand>
    {
        private readonly string[] _allowedExtensions =
        {
            ".pdf",
            ".docx"
        };

        public UploadResumeCommandValidator()
        {
            RuleFor(x => x.File)
                .NotNull();

            RuleFor(x => x.File.Length)
                .LessThanOrEqualTo(5 * 1024 * 1024);

            RuleFor(x => x.File.FileName)
                .Must(HaveValidExtension)
                .WithMessage("Only PDF and DOCX files are allowed.");
        }

        private bool HaveValidExtension(
            string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            return _allowedExtensions.Contains(extension);
        }
    }
}
