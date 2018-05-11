using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class UserShippingInputModel
    {
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }
}