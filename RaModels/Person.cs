using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;

namespace RaModels
{
    public class Person
    {
        public Person() => ContactInfos = new List<ContactInfo>();
        private string? _bsonId;
        private string? name;
        private string? surname;
        private string? companyName;

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
        public string? Name { get => name; set => name = value?.Trim(); }
        public string? Surname { get => surname; set => surname = value?.Trim(); }
        public string? CompanyName { get => companyName; set => companyName = value?.Trim(); }
        public List<ContactInfo> ContactInfos { get; set; }
    }
}
