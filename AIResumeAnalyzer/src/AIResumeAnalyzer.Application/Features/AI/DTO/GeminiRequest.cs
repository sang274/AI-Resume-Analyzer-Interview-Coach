using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.AI.DTO
{
    public class GeminiRequest
    {
        public GeminiContent[] contents { get; set; } = null!;
    }

    public class GeminiContent
    {
        public GeminiPart[] parts { get; set; } = null!;
    }

    public class GeminiPart
    {
        public string text { get; set; } = string.Empty;
    }
}
