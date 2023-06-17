namespace SupplyChainManagement.Models.CRUD
{
    public class CrudViewModel<T> where T : class
    {
        public string action { get; set; } = null!;
        public object key { get; set; } = null!;
        public string antiForgery { get; set; } = null!;
        public T value { get; set; } = null!;
    }
}
