using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class AuthorInputModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "You must enter the authors name")]
        public string Name { get; set; }
    }
}