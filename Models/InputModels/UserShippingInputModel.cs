using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class UserShippingInputModel
    {
        [Required]
       // [RegularExpression("^[_A-z0-9]((-|\\s)[_A-z0-9])*$", ErrorMessage = "Invalid street address")]
        public string StreetAddress { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }
}