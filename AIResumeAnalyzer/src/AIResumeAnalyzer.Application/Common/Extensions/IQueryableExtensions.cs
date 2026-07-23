using AIResumeAnalyzer.Application.Common.Filtering;
using AIResumeAnalyzer.Application.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AIResumeAnalyzer.Application.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySorting<T>(
            this IQueryable<T> query,
            SortParams sort,
            Dictionary<string, Expression<Func<T, object>>> columns,
            Expression<Func<T, object>> defaultSort)
        {
            if (sort == null)
                return query.OrderByDescending(defaultSort);

            if (string.IsNullOrWhiteSpace(sort.SortBy))
            {
                return sort.Direction == SortDirection.Asc
                    ? query.OrderBy(defaultSort)
                    : query.OrderByDescending(defaultSort);
            }

            if (!columns.TryGetValue(sort.SortBy.ToLower(), out var selector))
            {
                return sort.Direction == SortDirection.Asc
                    ? query.OrderBy(defaultSort)
                    : query.OrderByDescending(defaultSort);
            }

            return sort.Direction == SortDirection.Asc
                ? query.OrderBy(selector)
                : query.OrderByDescending(selector);
        }

        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query,
            PaginationParams pagination,
            CancellationToken cancellationToken)
        {
            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>
            {
                Items = items,
                Page = pagination.Page,
                PageSize = pagination.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize),
                HasPreviousPage = pagination.Page > 1,
                HasNextPage = pagination.Page < (int)Math.Ceiling(totalCount / (double)pagination.PageSize)
            };
        }
    }
}