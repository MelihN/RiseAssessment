using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaModels
{
    public class RequestModel<T>
    {
        public string RecordId { get; set; }
        public T Item { get; set; }
        public int PagingLength { get; set; }
        public int PagingStart { get; set; }
    }
}
