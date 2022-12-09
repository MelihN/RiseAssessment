using RaModels;
using RAUI.Models;
using RAUI.ServiceHelper;

namespace RAUI.BLL
{
    public class PersonBLL
    {
        private PersonService serviceCall = new PersonService();
        private string baseUrl; 
        public PersonBLL(string _baseUrl)
        {
            baseUrl = _baseUrl;
        }

        public async Task<Person> GetOne(RequestModel<Person> reqMsg)
        {
            var result = new ResponseModel<Person>();
            var ReqMsg = new RequestModel<Person>();
            result = await serviceCall.GetOne(ReqMsg, baseUrl);
            if (!result.ServiceState)
                throw new Exception(result.ErrorMsg);
            if (!result.RequestState)
                throw new Exception(result.ErrorMsg);
            return result.Item;
        }

        public async Task<DtoResponseModel<Person>> GetList(DtoModel reqMsg)
        {
            var result = new ResponseModel<Person>();
            var ReqMsg = new RequestModel<Person>() { PagingStart = reqMsg.Start, PagingLength = reqMsg.Length, Item = new Person() };
            result = await serviceCall.GetList(ReqMsg, baseUrl);

            if (!result.ServiceState)
                throw new Exception(result.ErrorMsg);
            if (!result.RequestState)
                throw new Exception(result.ErrorMsg);

            if (result.RequestState)
            {
                if (result?.ItemList?.Count > 0)
                {
                    return new DtoResponseModel<Person>()
                    {
                        data = result.ItemList,
                        draw = reqMsg.Draw,
                        recordsTotal = result.TotalRowCount,
                        recordsFiltered = result.ItemList.Count
                    };
                }
                else
                    return new DtoResponseModel<Person>() { draw = reqMsg.Draw };
            }
            else
            {
                return new DtoResponseModel<Person>() { draw = reqMsg.Draw };
            }
        }

        public async Task<ResponseModel<Person>> Create(RequestModel<Person> reqModel)
        {
            var result = new ResponseModel<Person>();
            if (reqModel.Item != null && string.IsNullOrWhiteSpace(reqModel.Item.Id))
            {
                reqModel.Item.UUID = Guid.NewGuid().ToString();
                result = await serviceCall.Create(reqModel, baseUrl);
            }
            else
            {
                reqModel.RecordId = reqModel?.Item.Id;
                result = await serviceCall.Update(reqModel, baseUrl);
            }
                
            return result;
        }
        
        public async Task<ResponseModel<Person>> Delete(RequestModel<Person> reqMsg)
        {
            var result = new ResponseModel<Person>();            
            result = await serviceCall.Delete(reqMsg, baseUrl);
            return result;
        }
    }
}
