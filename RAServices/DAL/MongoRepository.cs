using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using RAServices.Model;
using System.Xml.Linq;

namespace RAServices.DAL
{
    public class MongoRepository : IMongoRepository, IDisposable
    {
        //private string connectionString;
        //private string dbName;
        IMongoDatabase database;
        private string collectionName;

        public MongoRepository(string _connectionString, string _dbName, string _collectionName)
        {            
            MongoClient dbClient = new MongoClient(_connectionString);
            database = dbClient.GetDatabase(_dbName);
            this.collectionName = _collectionName;
        }

        public async Task<DbModel<T>> GetFindOne<T>(DbModel<T> requestModel)
        {
            var result = new DbModel<T>();
            
            var collection = database.GetCollection<T>(collectionName);

            var resultData = default(T);
            int dataCount = 0;
            try
            {
                resultData = await collection.Find(Builders<T>.Filter.Eq("_id", requestModel.RecordID)).FirstOrDefaultAsync<T>();
            }
            catch (Exception ex)
            {
                result.ErrorMsg = $"Mongo Select Exception: {ex.Message}{Environment.NewLine} InnerException{(ex.InnerException != null ? ex.InnerException.Message : string.Empty)} {Environment.NewLine} Data : {resultData} ";
                result.State = false;
            }

            result.TotalRowCount = dataCount;

            if (resultData != null)
            {
                result.Item = resultData;
                result.State = true;
            }
            else if (string.IsNullOrWhiteSpace(result.ErrorMsg))
            {
                result.State = true;
                result.ErrorMsg = "Veri bulunamadı.";
                result.ResultList = new List<T>();
            }
            return result;
        }

        public  async Task<DbModel<T>> GetDataList<T>(DbModel<T> requestModel)
        {
            var result = new DbModel<T>();            
            var collection = database.GetCollection<T>(collectionName);

            var resultData = new List<T>();
            long dataCount = 0;
            try
            {
                var allDocument = collection.Find(_ => true);
                result.TotalRowCount = allDocument.ToList().Count;

                if (requestModel.PagingStart >= 0 && requestModel.PagingLength > 0)
                {
                    resultData = await allDocument.Skip(requestModel.PagingStart).Limit(requestModel.PagingLength).ToListAsync();
                }
                else
                {
                    resultData = await allDocument.ToListAsync();
                }
            }
            catch (MongoException mgDbEx)
            {
                result.ErrorMsg = $"Mongo Select MongoException: {mgDbEx.Message}{Environment.NewLine} InnerException{(mgDbEx.InnerException != null ? mgDbEx.InnerException.Message : string.Empty)} {Environment.NewLine} Data : {resultData} ";
                result.State = false;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = $"Mongo Select Exception: {ex.Message}{Environment.NewLine} InnerException{(ex.InnerException != null ? ex.InnerException.Message : string.Empty)} {Environment.NewLine} Data : {resultData} ";
                result.State = false;
            }

            if (resultData != null && resultData.Count > 0)
            {
                result.ResultList = resultData;
                result.State = true;
            }
            else
            {
                result.State = true;
                result.ErrorMsg = "Veri bulunamadı.";
                result.ResultList = new List<T>();
            }
            return result;
        }

        public async Task<DbModel<T>> InsertData<T>(DbModel<T> request)
        {
            var result = new DbModel<T>();            
            var collection = database.GetCollection<T>(collectionName);
            string jsonString = string.Empty;
            try
            {                
                var filterDef = Builders<T>.Filter.Eq("_id", request.RecordID);
                var avilableT = await collection.Find<T>(filterDef).ToListAsync();
                if (avilableT.Count <= 0)
                {
                    collection.InsertOneAsync(request.Item);
                    var lastInsert = collection.Find<T>(filterDef).ToList();
                    result.State = true;
                    if (lastInsert != null && lastInsert.Count > 0)
                    {
                        result.Item = lastInsert.FirstOrDefault();
                    }
                    else
                    {
                        result.Item = request.Item;
                    }

                }
                else
                {
                    result.State = false;
                    result.ErrorMsg = "Mongo Insert : Object is available.";
                }

            }
            catch (MongoException mex)
            {
                result.State = false;
                result.ErrorMsg = $"Mongo Insert MongoException:{mex.Message} {Environment.NewLine} Inner Exception: {(mex.InnerException != null ? mex.InnerException.Message : string.Empty)} {Environment.NewLine} Data :{jsonString}";
            }
            catch (Exception ex)
            {
                result.State = false;
                result.ErrorMsg = $"Mongo Insert Exception:{ex.Message} {Environment.NewLine} Inner Exception: {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)} {Environment.NewLine} Data :{jsonString}";
            }

            return result;
        }

        public async Task<DbModel<T>> UpdateData<T>(DbModel<T> requestModel)
        {
            var result = new DbModel<T>();            
            var collection = database.GetCollection<T>(collectionName);

            try
            {
                ObjectId recordId = new ObjectId(requestModel.RecordID);
                var filterDef = Builders<T>.Filter.Eq("_id", recordId);
                var data = await collection.FindOneAndReplaceAsync<T>(filterDef,  requestModel.Item);
                result.State = true;
                result.Item = requestModel.Item;

            }
            catch (MongoException mex)
            {
                result.State = false;
                result.ErrorMsg = $"Mongo Update MongoException:{mex.Message} {Environment.NewLine} Inner Exception: {(mex.InnerException != null ? mex.InnerException.Message : string.Empty)} {Environment.NewLine}";
            }
            catch (Exception ex)
            {
                result.State = false;
                result.ErrorMsg = $"Mongo Update Exception:{ex.Message} {Environment.NewLine} Inner Exception: {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)} {Environment.NewLine}";
            }

            return result;
        }

        public async Task<DbModel<T>> DeleteData<T>(DbModel<T> requestModel)
        {
            var result = new DbModel<T>();            
            var collection = database.GetCollection<T>(collectionName);

            try
            {
                ObjectId recordId = new ObjectId(requestModel.RecordID);
                var filterDef = Builders<T>.Filter.Eq("_id", recordId);
                var deleteAction = await collection.DeleteOneAsync(filterDef);
                result.State = (deleteAction.DeletedCount == 1 ? true : false);
            }
            catch (MongoException mex)
            {
                result.State = false;
                result.ErrorMsg = $"Mongo Delete MongoException:{mex.Message} {Environment.NewLine} Inner Exception: {(mex.InnerException != null ? mex.InnerException.Message : string.Empty)} {Environment.NewLine} ";
            }
            catch (Exception ex)
            {
                result.State = false;
                result.ErrorMsg = $"Mongo Delete Exception:{ex.Message} {Environment.NewLine} Inner Exception: {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)} {Environment.NewLine} ";
            }

            return result;
        }

        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
