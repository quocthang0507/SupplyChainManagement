namespace ApplicationCore.Interfaces
{
    public interface IPagedList
    {
        /// <summary>
        /// Tổng số trang (số tập con)
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Tổng số phần tử trả về từ truy vấn
        /// </summary>
        long TotalItemCount { get; }

        /// <summary>
        /// Chỉ số trang hiện tại (bắt đầu từ 0)
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Vị trí của trang hiện tại (bắt đầu từ 1)
        /// </summary>
        int PageNumber { get; }

        /// <summary>
        /// Số lượng phần tử tối đa trên 1 trang
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Kiểm tra có trang trước hay không
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Kiểm tra có trang tiếp theo hay không
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        /// Trang hiện tại có phải là trang đầu tiên không
        /// </summary>
        bool IsFirstPage { get; }

        /// <summary>
        /// Trang hiện tại có phải là trang cuối cùng không
        /// </summary>
        bool IsLastPage { get; }

        /// <summary>
        /// Thứ tự của phần tử đầu trang trong truy vấn (bắt đầu từ 1)
        /// </summary>
        int FirstItemIndex { get; }

        /// <summary>
        /// Thứ tự của phần tử cuối trang trong truy vấn (bd từ 1)
        /// </summary>
        int LastItemIndex { get; }
    }

    public interface IPagedList<out T> : IPagedList, IEnumerable<T>
    {
        /// <summary>
        /// Lấy phần tử tại vị trí index (bắt đầu từ 0)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        T this[int index] { get; }

        /// <summary>
        /// Đếm số lượng phần tử chứa trong trang
        /// </summary>
        int Count { get; }
    }
}
