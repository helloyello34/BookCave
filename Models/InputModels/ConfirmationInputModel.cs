using System;
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class ConfirmationInputModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression("[0-9][0-9][0-9]", ErrorMessage = "Enter a 3-digit CVC number")]
        public int CVC { get; set; }
        [Required]
        [RegularExpression("[0-9]{16}", ErrorMessage = "Enter a valid credit card number")]
        public string CreditCard { get; set; }
        [Required]
        public  int Year { get; set; }
        [Required]
        public  int Month { get; set; }
    }
}