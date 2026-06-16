using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Interfaces.IFileService
{
    public interface IFileStorageService
    {
        Task<string> SaveResumeAsync(
            IFormFile file,
            CancellationToken cancellationToken);
    }
}
