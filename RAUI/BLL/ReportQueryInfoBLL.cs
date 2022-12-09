using RaModels;
using RAUI.Models;
using RAUI.ServiceHelper;

namespace RAUI.BLL
{
    public class ReportQueryInfoBLL
    {
        private ReportQueryInfoService serviceCall = new ReportQueryInfoService();
        private string baseUrl;
        public ReportQueryInfoBLL(string _baseUrl)
        {
            baseUrl = _baseUrl;
        }

        public async Task<ReportQueryInfo> GetOne(RequestModel<ReportQueryInfo> reqMsg)
        {
            var result = new ResponseModel<ReportQueryInfo>();
            var ReqMsg = new RequestModel<ReportQueryInfo>();
            result = await serviceCall.GetOne(ReqMsg, baseUrl);
            if (!result.ServiceState)
                throw new Exception(result.ErrorMsg);
            if (!result.RequestState)
                throw new Exception(result.ErrorMsg);
            return result.Item;
        }

        public async Task<DtoResponseModel<ReportQueryInfo>> GetList(DtoModel reqMsg)
        {
            var result = new ResponseModel<ReportQueryInfo>();
            var ReqMsg = new RequestModel<ReportQueryInfo>() { PagingStart = reqMsg.Start, PagingLength = reqMsg.Length, Item = new ReportQueryInfo() };
            result = await serviceCall.GetList(ReqMsg, baseUrl);

            if (!result.ServiceState)
                throw new Exception(result.ErrorMsg);
            if (!result.RequestState)
                throw new Exception(result.ErrorMsg);

            if (result.RequestState)
            {
                if (result?.ItemList?.Count > 0)
                {
                    return new DtoResponseModel<ReportQueryInfo>()
                    {
                        data = result.ItemList,
                        draw = reqMsg.Draw,
                        recordsTotal = result.TotalRowCount,
                        recordsFiltered = result.ItemList.Count
                    };
                }
                else
                    return new DtoResponseModel<ReportQueryInfo>() { draw = reqMsg.Draw };
            }
            else
            {
                return new DtoResponseModel<ReportQueryInfo>() { draw = reqMsg.Draw };
            }
        }

        public async Task<ResponseModel<ReportQueryInfo>> Create(RequestModel<ReportQueryInfo> reqModel)
        {
            var result = new ResponseModel<ReportQueryInfo>();
            if (reqModel.Item != null && string.IsNullOrWhiteSpace(reqModel.RecordId))
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

        public async Task<ResponseModel<ReportQueryInfo>> Delete(RequestModel<ReportQueryInfo> reqMsg)
        {
            var result = new ResponseModel<ReportQueryInfo>();
            result = await serviceCall.Delete(reqMsg, baseUrl);
            return result;
        }
    }
}
