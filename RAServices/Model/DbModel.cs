namespace RAServices.Model
{
    public class DbModel<T> 
    {     
        public T Item { get; set; }
        public List<T> ResultList { get; set; }
        public string RecordID { get; set; }                
        public string ErrorMsg { get; set; }
        public bool State { get; set; }
        public int PagingLength { get; set; }
        public int PagingStart { get; set; }
        public long TotalRowCount { get; set; }
    }
}
