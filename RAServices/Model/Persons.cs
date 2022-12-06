namespace RAServices.Model
{
    public class Persons
    {
        //Mongo DB UUID formatında tutmaktadır
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public List<ContactInfo> ContactInfos { get; set; }
    }
}
