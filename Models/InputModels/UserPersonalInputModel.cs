using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class UserPersonalInputModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }

    }
}