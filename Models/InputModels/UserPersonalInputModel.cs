using System;
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
        public string ImageUrl { get; set; }
        public DateTime Birthday { get; set; }
        public string FavoriteBook { get; set; }

    }
}