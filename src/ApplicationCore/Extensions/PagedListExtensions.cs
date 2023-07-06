using ApplicationCore.Interfaces;
using ApplicationCore.ResponseModels;
using MongoDB.Driver;

namespace ApplicationCore.Extensions
{
    public static class PagedListExtensions
    {
        public static SortDefinition<T> GetOrder<T>(this IPagingParams pagingParams, string defaultProperty)
        {
            if (pagingParams.SortOrder.Equals("ASC", StringComparison.OrdinalIgnoreCase))
                return Builders<T>.Sort.Ascending(obj => obj.GetType().GetProperty(defaultProperty).GetValue(obj));
            return Builders<T>.Sort.Descending(obj => obj.GetType().GetProperty(defaultProperty).GetValue(obj));
        }

        public static async Task<IPagedList<T>> ToPagedListAsync<T>(
            this IFindFluent<T, T> source,
            IPagingParams pagingParams)
        {
            var totalCount = await source.CountDocumentsAsync();
            var items = await source
                .Sort(pagingParams.GetOrder<T>(pagingParams.SortingProperty))
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
