using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RaModels
{
    public class ReportQueryInfo
    {
        private string? _bsonId;
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id
        {
            get { return this._bsonId; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this._bsonId = new BsonObjectId(ObjectId.GenerateNewId()).ToString();
                }
                else
                    this._bsonId = value;
            }
        }
        public string UUID { get; set; } = string.Empty;
        public DateTime? RequestDate { get; set; }
        public string? ReportState { get; set; }
    }
}
