using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaModels
{
    
    public class ReportQueryState
    {
        public string StateName { get; set; }
        public string StateValue { get; set; }        
    }

    public static class ReportQueryStates
    {
        public static List<ReportQueryState> reportQueryStates = new List<ReportQueryState>()
        {
            new ReportQueryState { StateValue = "S1", StateName= "Sorgu Yapılıyor"},
            new ReportQueryState { StateValue = "S2", StateName = "Hazırlanıyor"},
            new ReportQueryState { StateValue = "S3", StateName = "Tamamlandı"}
        };
    }

}
