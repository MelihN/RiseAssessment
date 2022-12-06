using MongoDB.Bson;
using MongoDB.Driver;
using RAServices.Model;
using System.Globalization;

namespace RAServices.DAL
{
    public class MongoDbAccess : IDisposable
    {
        public DbModel<T> GetFindOne<T>(DbModel<T> requestModel)
        {
            var result = new DbModel<T>();
            MongoClient dbClient = new MongoClient(requestModel.ConnectionString);
            var database = dbClient.GetDatabase(requestModel.DbName);
            var collection = database.GetCollection<T>(requestModel.CollectionName);

            var resultData = default(T);
            int dataCount = 0;
            try
            {
                resultData = collection.Find(_ => true).FirstOrDefault<T>();
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

        public DbModel<T> GetDataList<T>(DbModel<T> requestModel)
        {
            var result = new DbModel<T>();
            MongoClient dbClient = new MongoClient(requestModel.ConnectionString);
            var database = dbClient.GetDatabase(requestModel.DbName);
            var collection = database.GetCollection<T>(requestModel.CollectionName);

            var resultData = new List<T>();
            long dataCount = 0;
            try
            {
                var allDocument = collection.Find(_ => true);
                result.TotalRowCount = allDocument.ToList().Count;

                if (requestModel.PagingStart >= 0 && requestModel.PagingLength > 0)
                {
                    resultData = allDocument.Skip(requestModel.PagingStart).Limit(requestModel.PagingLength).ToList();
                }
                else
                {
                    resultData = allDocument.ToList();
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

        public DbModel<T> InsertData<T>(DbModel<T> request)
        {
            var result = new DbModel<T>();
            MongoClient dbClient = new MongoClient(request.ConnectionString);
            var database = dbClient.GetDatabase(request.DbName);
            var collection = database.GetCollection<T>(request.CollectionName);
            string jsonString = string.Empty;
            try
            {
                var filterDef = Builders<T>.Filter.Eq("_id", request.RecordID);
                var avilableT = collection.Find<T>(filterDef).ToList();
                if (avilableT.Count <= 0)
                {
                    collection.InsertOne(request.Item);
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

        public DbModel<T> UpdateData<T>(DbModel<T> requestModel)
        {
            var result = new DbModel<T>();
            MongoClient dbClient = new MongoClient(requestModel.ConnectionString);
            var database = dbClient.GetDatabase(requestModel.DbName);
            var collection = database.GetCollection<T>(requestModel.CollectionName);

            try
            {
                var filterDef = Builders<T>.Filter.Eq("_id", requestModel.RecordID);
                var data = collection.FindOneAndReplace<T>(filterDef, requestModel.Item);
                result.State = true;
                result.Item = data;

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

        public DbModel<T> DeleteData<T>(DbModel<T> requestModel)
        {
            var result = new DbModel<T>();
            MongoClient dbClient = new MongoClient(requestModel.ConnectionString);
            var database = dbClient.GetDatabase(requestModel.DbName);
            var collection = database.GetCollection<T>(requestModel.CollectionName);

            try
            {
                var filterDef = Builders<T>.Filter.Eq("_id", requestModel.RecordID);
                collection.DeleteMany(filterDef);
                result.State = true;

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
