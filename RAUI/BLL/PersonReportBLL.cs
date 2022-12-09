using RaModels;
using RAUI.Models;
using RAUI.ServiceHelper;

namespace RAUI.BLL
{
    public class PersonReportBLL
    {
        private PersonReportService serviceCall = new PersonReportService();
        private string baseUrl;
        public PersonReportBLL(string _baseUrl)
        {
            baseUrl = _baseUrl;
        }

        public async Task<DtoResponseModel<PersonReport>> GetReportData(DtoModel reqMsg)
        {
            var repState = await SetReportState(ReportQueryStates.queryState.S1.ToString(), null);
            var result = new ResponseModel<PersonReport>();
            var ReqMsg = new RequestModel<PersonReport>() { PagingStart = reqMsg.Start, PagingLength = reqMsg.Length, Item = new PersonReport() };
            
            repState = await SetReportState(ReportQueryStates.queryState.S2.ToString(), repState.Item);
            result = await serviceCall.GetReportData(ReqMsg, baseUrl);

            if (!result.ServiceState)
                throw new Exception(result.ErrorMsg);
            if (!result.RequestState)
                throw new Exception(result.ErrorMsg);

            if (result.RequestState)
            {
                
                if (result.ItemList.Count > 0)
                {
                    repState = await SetReportState(ReportQueryStates.queryState.S3.ToString(), repState.Item);
                    return new DtoResponseModel<PersonReport>()
                    {
                        data = result.ItemList,
                        draw = reqMsg.Draw,
                        recordsTotal = result.TotalRowCount,
                        recordsFiltered = result.ItemList.Count
                    };
                }
                else
                {
                    
                    return new DtoResponseModel<PersonReport>() { draw = reqMsg.Draw };
                }                    
            }
            else
            {
                repState = await SetReportState(ReportQueryStates.queryState.S9.ToString(), repState.Item);
                return new DtoResponseModel<PersonReport>() { draw = reqMsg.Draw };
            }
        }

        public async Task<ResponseModel<ReportQueryInfo>> SetReportState(string qState, ReportQueryInfo model)
        {
            var bll = new ReportQueryInfoBLL(baseUrl);
            string UUID = model?.UUID;
            if (qState == ReportQueryStates.queryState.S1.ToString())
                UUID = Guid.NewGuid().ToString();

            var result = await bll.Create(new RequestModel<ReportQueryInfo>()
            {
                Item = new ReportQueryInfo()
                {
                    Id = model?.Id,
                    RequestDate = DateTime.Now,
                    ReportState = qState,
                    UUID = UUID
                },
                 RecordId = model?.Id ?? string.Empty
            });
            return result;
        }
    }
}
