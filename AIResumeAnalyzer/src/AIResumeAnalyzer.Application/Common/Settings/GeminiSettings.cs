using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Common.Settings
{
    public class GeminiSettings
    {
        public string ApiKey { get; set; } = string.Empty;

        public string Model { get; set; } = "gemini-1.5-flash";
    }
}
