using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaModels
{
    public class ResponseModel<T>
    {
        public bool ServiceState { get; set; }
        public bool RequestState { get; set; }
        public string ErrorMsg { get; set; }
        public List<T> ItemList { get; set; }
        public T Item { get; set; }
        public long TotalRowCount { get; set; }
    }
}
