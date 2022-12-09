using RaModels;
using RAUI.Models;
using System.Text;

namespace RAUI.ServiceHelper
{
    public class PersonService : ServiceBase
    {
        public async Task<ResponseModel<Person>> GetList(RequestModel<Person> requestModel, string baseUrl)
        {
            var client = ServiceClient(baseUrl);
            using (HttpResponseMessage response = await client.PostAsJsonAsync("api/Persons/GetList", requestModel))
            {
                var result = new ResponseModel<Person>();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ResponseModel<Person>>();
                }
                else
                {
                    result.ErrorMsg = CheckServiceStatusError(response.StatusCode);
                }
                return result;
            }
            return new ResponseModel<Person>();
        }

        public async Task<ResponseModel<Person>> GetOne(RequestModel<Person> ReqMsg, string baseUrl)
        {
            var client = ServiceClient(baseUrl);

            using (HttpResponseMessage response = await client.PostAsJsonAsync("api/Persons/GetOne", ReqMsg))
            {
                var result = new ResponseModel<Person>();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ResponseModel<Person>>();
                }
                else
                {
                    result.ErrorMsg = CheckServiceStatusError(response.StatusCode);
                }
                return result;
            }
        }

        public async Task<ResponseModel<Person>> Create(RequestModel<Person> ReqMsg, string baseUrl)
        {
            var client = ServiceClient(baseUrl);

            using (HttpResponseMessage response = await client.PostAsJsonAsync("api/Persons/Create", ReqMsg))
            {
                var result = new ResponseModel<Person>();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ResponseModel<Person>>();
                }
                else
                {
                    result.ErrorMsg = CheckServiceStatusError(response.StatusCode);
                }
                return result;
            }
        }

        public async Task<ResponseModel<Person>> Update(RequestModel<Person> ReqMsg, string baseUrl)
        {
            var client = ServiceClient(baseUrl);

            using (HttpResponseMessage response = await client.PostAsJsonAsync("api/Persons/Update", ReqMsg))
            {
                var result = new ResponseModel<Person>();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ResponseModel<Person>>();
                }
                else
                {
                    result.ErrorMsg = CheckServiceStatusError(response.StatusCode);
                }
                return result;
            }
        }

        public async Task<ResponseModel<Person>> Delete(RequestModel<Person> ReqMsg, string baseUrl)
        {
            var client = ServiceClient(baseUrl);

            using (HttpResponseMessage response = await client.PostAsJsonAsync("api/Persons/Delete", ReqMsg))
            {
                var result = new ResponseModel<Person>();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<ResponseModel<Person>>();
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
