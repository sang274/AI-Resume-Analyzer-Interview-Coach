using AIResumeAnalyzer.Application.Interfaces.IFileService;
using System.Text;
using UglyToad.PdfPig;

namespace AIResumeAnalyzer.Infrastructure.Services.FileService
{
    public class ResumeParserService : IResumeParserService
    {
        public Task<string> ParsePdfAsync(
            string filePath,
            CancellationToken cancellationToken)
        {
            using var document =
                PdfDocument.Open(filePath);

            var text = new StringBuilder();

            foreach (var page in document.GetPages())
            {
                text.AppendLine(page.Text);
            }

            return Task.FromResult(
                text.ToString());
        }
    }
}
