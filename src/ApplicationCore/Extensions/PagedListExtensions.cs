using ApplicationCore.Interfaces;
using ApplicationCore.ResponseModels;
using MongoDB.Driver;

namespace ApplicationCore.Extensions
{
    public static class PagedListExtensions
    {
        public static string GetOrder(this IPagingParams pagingParams, string defaultProperty = "Id")
        {
            var property = string.IsNullOrWhiteSpace(pagingParams.SortingProperty)
                ? defaultProperty
                : pagingParams.SortingProperty;

            var order = "ASC".Equals(
                pagingParams.SortOrder, StringComparison.OrdinalIgnoreCase)
                ? 1 : -1;

            return $"{{{property}: {order}}}";
        }

        public static async Task<IPagedList<T>> ToPagedListAsync<T>(
            this IFindFluent<T, T> source,
            IPagingParams pagingParams)
        {
            var totalCount = await source.CountDocumentsAsync();
            var items = await source
                .Sort(pagingParams.GetOrder())
                .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
                .Limit(pagingParams.PageSize)
                .ToListAsync();

            return new PagedList<T>(
                items,
                pagingParams.PageNumber,
                pagingParams.PageSize,
                totalCount);
        }
    }
}
