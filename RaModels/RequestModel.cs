namespace RaModels
{
    public class RequestModel<T>
    {
        public string RecordId { get; set; } = string.Empty;
        public T Item { get; set; }
        public int PagingLength { get; set; }
        public int PagingStart { get; set; }
    }
}
