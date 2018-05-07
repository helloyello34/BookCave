using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class CommentInputModel
    {

        [Required]
        public string Comment { get; set; }
        [Required]
        public double Rating { get; set; }
    }
}