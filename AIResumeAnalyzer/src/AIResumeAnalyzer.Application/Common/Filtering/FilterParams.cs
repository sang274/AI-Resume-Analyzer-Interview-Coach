using AIResumeAnalyzer.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Common.Filtering
{
    public class FilterParams : PaginationParams
    {
        public string? Keyword { get; set; }

        public SortParams Sort { get; set; } = new();
    }
}
