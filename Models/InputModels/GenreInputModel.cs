using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class GenreInputModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "You must enter a genre name")]
        public string Genre { get; set; }
    }
}