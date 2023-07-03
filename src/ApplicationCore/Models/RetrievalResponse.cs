namespace ApplicationCore.Models
{
    public class RetrievalResponse<T>
    {
        public List<T> Items { get; set; }
        public int Count { get => Items.Count; }

        public RetrievalResponse(List<T> items)
        {
            Items = items;
        }
    }
}
