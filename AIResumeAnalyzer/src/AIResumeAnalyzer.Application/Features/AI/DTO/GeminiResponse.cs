using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.AI.DTO
{
    public class GeminiResponse
    {
        public GeminiCandidate[] Candidates { get; set; } = null!;
    }

    public class GeminiCandidate
    {
        public GeminiResponseContent Content { get; set; } = null!;
    }

    public class GeminiResponseContent
    {
        public GeminiPart[] Parts { get; set; } = null!;
    }
}
