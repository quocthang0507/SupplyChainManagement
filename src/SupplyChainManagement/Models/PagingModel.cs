using ApplicationCore.Interfaces;

namespace SupplyChainManagement.Models
{
    public class PagingModel : IPagingParams
    {
        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; } = 1;

        public string SortingProperty { get; set; } = null!;

        public string SortOrder { get; set; } = null!;
    }
}
