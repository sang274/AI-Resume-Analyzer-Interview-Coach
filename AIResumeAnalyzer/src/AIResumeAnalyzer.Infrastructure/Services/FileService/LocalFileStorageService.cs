using AIResumeAnalyzer.Application.Interfaces.IFileService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Infrastructure.Services.FileService
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;

        public LocalFileStorageService(
            IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveResumeAsync(
            IFormFile file,
            CancellationToken cancellationToken)
        {
            var uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", "resumes");

            Directory.CreateDirectory(uploadFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var fullPath = Path.Combine(uploadFolder, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);

            await file.CopyToAsync(stream, cancellationToken);

            return $"/uploads/resumes/{fileName}";
        }

        public string GetPhysicalPath(string fileUrl)
        {
            return Path.Combine(_environment.WebRootPath, fileUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
        }
    }
}
