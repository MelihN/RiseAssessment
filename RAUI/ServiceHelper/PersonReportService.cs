using RaModels;

namespace RAUI.ServiceHelper
{
    public class PersonReportService : ServiceBase
    {
        public async Task<ResponseModel<PersonReport>> GetReportData(RequestModel<PersonReport> requestModel, string baseUrl)
        {
            var client = ServiceClient(baseUrl);
            using (HttpResponseMessage response = await client.PostAsJsonAsync("api/PersonReport/GetReportData", requestModel))
            {
                var result = new ResponseModel<PersonReport>();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ResponseModel<PersonReport>>();
                }
                else
                {
                    result.ErrorMsg = CheckServiceStatusError(response.StatusCode);
                }
                return result;
            }
            return new ResponseModel<PersonReport>();
        }
    }
}
