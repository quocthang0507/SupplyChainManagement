﻿using ApplicationCore.Interfaces;

namespace ApplicationCore.ResponseModels
{
    public class PagingMetadata : IPagedList
    {
        protected PagingMetadata(int pageNumber, int pageSize, long totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItemCount = totalCount;
        }

        public PagingMetadata(IPagedList pagedList)
        {
            TotalItemCount = pagedList.TotalItemCount;
            PageIndex = pagedList.PageIndex;
            PageSize = pagedList.PageSize;
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; }

        public long TotalItemCount { get; }

        public int PageNumber
        {
            get => PageIndex + 1;
            set => PageIndex = value - 1;
        }

        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                    return 0;

                var total = TotalItemCount / PageSize;

                if (TotalItemCount % PageSize > 0)
                    total++;

                return (int)total;
            }
        }

        public bool HasPreviousPage => PageIndex > 0;

        public bool HasNextPage => (PageIndex < (PageCount - 1));

        public int FirstItemIndex => (PageIndex * PageSize) + 1;

        public int LastItemIndex
            => (int)Math.Min(TotalItemCount, ((PageIndex * PageSize) + PageSize));

        public bool IsFirstPage => (PageIndex <= 0);

        public bool IsLastPage => (PageIndex >= (PageCount - 1));
    }
}
