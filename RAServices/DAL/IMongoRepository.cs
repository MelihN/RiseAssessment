using RAServices.Model;

namespace RAServices.DAL
{
    public interface IMongoRepository
    {
        Task<DbModel<T>> GetFindOne<T>(DbModel<T> requestModel);
        Task<DbModel<T>> GetDataList<T>(DbModel<T> requestModel);
        Task<DbModel<T>> InsertData<T>(DbModel<T> request);
        Task<DbModel<T>> UpdateData<T>(DbModel<T> requestModel);
        Task<DbModel<T>> DeleteData<T>(DbModel<T> requestModel);
    }
}
