using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Common.Filtering
{
    public class SortParams
    {
        public string? SortBy { get; set; }

        public SortDirection Direction { get; set; } = SortDirection.Desc;
    }
}
