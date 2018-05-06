namespace BookCave.Models.InputModels
{
    public class UserShippingInputModel
    {
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}