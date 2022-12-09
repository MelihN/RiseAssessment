using RaModels;

namespace RAUI.ServiceHelper
{
    public class ReportQueryInfoService : ServiceBase
    {
        public async Task<ResponseModel<ReportQueryInfo>> GetList(RequestModel<ReportQueryInfo> requestModel, string baseUrl)
        {
            var client = ServiceClient(baseUrl);
            using (HttpResponseMessage response = await client.PostAsJsonAsync("api/ReportState/GetList", requestModel))
            {
                var result = new ResponseModel<ReportQueryInfo>();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ResponseModel<ReportQueryInfo>>();
                }
                else
                {
                    result.ErrorMsg = CheckServiceStatusError(response.StatusCode);
                }
                return result;
            }
            return new ResponseModel<ReportQueryInfo>();
        }

        public async Task<ResponseModel<ReportQueryInfo>> GetOne(RequestModel<ReportQueryInfo> ReqMsg, string baseUrl)
        {
            var client = ServiceClient(baseUrl);

            using (HttpResponseMessage response = await client.PostAsJsonAsync("api/ReportState/GetOne", ReqMsg))
            {
                var result = new ResponseModel<ReportQueryInfo>();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ResponseModel<ReportQueryInfo>>();
                }
                else
                {
                    result.ErrorMsg = CheckServiceStatusError(response.StatusCode);
                }
                return result;
            }
        }

        public async Task<ResponseModel<ReportQueryInfo>> Create(RequestModel<ReportQueryInfo> ReqMsg, string baseUrl)
        {
            var client = ServiceClient(baseUrl);

            using (HttpResponseMessage response = await client.PostAsJsonAsync("api/ReportState/Create", ReqMsg))
            {
                var result = new ResponseModel<ReportQueryInfo>();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ResponseModel<ReportQueryInfo>>();
                }
                else
                {
                    result.ErrorMsg = CheckServiceStatusError(response.StatusCode);
                }
                return result;
            }
        }

        public async Task<ResponseModel<ReportQueryInfo>> Update(RequestModel<ReportQueryInfo> ReqMsg, string baseUrl)
        {
            var client = ServiceClient(baseUrl);

            using (HttpResponseMessage response = await client.PostAsJsonAsync("api/ReportState/Update", ReqMsg))
            {
                var result = new ResponseModel<ReportQueryInfo>();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ResponseModel<ReportQueryInfo>>();
                }
                else
                {
                    result.ErrorMsg = CheckServiceStatusError(response.StatusCode);
                }
                return result;
            }
        }

        public async Task<ResponseModel<ReportQueryInfo>> Delete(RequestModel<ReportQueryInfo> ReqMsg, string baseUrl)
        {
            var client = ServiceClient(baseUrl);

            using (HttpResponseMessage response = await client.PostAsJsonAsync("api/ReportState/Delete", ReqMsg))
            {
                var result = new ResponseModel<ReportQueryInfo>();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ResponseModel<ReportQueryInfo>>();
                }
                else
                {
                    result.ErrorMsg = CheckServiceStatusError(response.StatusCode);
                }
                return result;
            }
        }
    }
}
