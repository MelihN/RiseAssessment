namespace RAUI.Models
{
    public class DtoResponseModel<T>
    {
        public List<T> data { get; set; }
        public int draw { get; set; }
        public long recordsTotal { get; set; }
        public int recordsFiltered { get; set; }

    }
    public class DtoModel
    {
        public int Draw { get; set; }
        public List<Columns> Columns { get; set; }
        public List<Order> Order { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public Search Search { get; set; }
    }

    public class Columns
    {
        public int Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public Search Search { get; set; }
        public string SearchValue { get; set; }
        public bool SearchRegex { get; set; }
    }

    public class Search
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }

    public class Order
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public class DatatableFilter
    {
        public string ReceiptType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string ReceiptNo { get; set; }
        public double MinAmount { get; set; }
        public double MaxAmount { get; set; }
        public string OperationCurrency { get; set; }
    }
}
