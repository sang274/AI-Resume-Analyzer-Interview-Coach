using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Interfaces.IFileService
{
    public interface IResumeParserService
    {
        Task<string> ParsePdfAsync(string filePath, CancellationToken cancellationToken);
    }
}
