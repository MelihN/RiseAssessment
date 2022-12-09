namespace RaModels
{
    public class ContactInfo
    {
        private string? phoneNumber;
        private string? email;
        private string? location;

        public string? PhoneNumber { get => phoneNumber; set => phoneNumber = value?.Trim(); }
        public string? Email { get => email; set => email = value?.Trim(); }
        public string? Location { get => location; set => location = value?.Trim(); }
    }
}
