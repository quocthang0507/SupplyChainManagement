namespace ApplicationCore.Interfaces
{
    public interface IPagingParams
    {
        /// <summary>
        /// Số mẫu tin trên một trang
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Số trang tính bắt đầu từ 1
        /// </summary>
        int PageNumber { get; set; }

        /// <summary>
        /// Tên cột muốn sắp xếp
        /// </summary>
        string SortingProperty { get; set; }

        /// <summary>
        /// Thứ tự sắp xếp: tăng hay giảm
        /// </summary>
        string SortOrder { get; set; }
    }
}
