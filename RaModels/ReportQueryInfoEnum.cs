namespace RaModels
{

    public class ReportQueryState
    {
        public string StateName { get; set; }
        public string StateValue { get; set; }        
    }

    public static class ReportQueryStates
    {
        public enum queryState
        {
            S1, S2, S3, S9
        }

        public static List<ReportQueryState> reportQueryStates = new List<ReportQueryState>()
        {
            new ReportQueryState { StateValue = "S1", StateName= "Sorgu Yapılıyor"},
            new ReportQueryState { StateValue = "S2", StateName = "Hazırlanıyor"},
            new ReportQueryState { StateValue = "S3", StateName = "Tamamlandı"},
            new ReportQueryState { StateValue = "S9", StateName = "Hata Oluştu"}
        };
    }

}
