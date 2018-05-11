using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class BookInputModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "You need to enter the ISBN number")] 
        public string ISBN { get; set; }
        [Required (ErrorMessage = "Type in language")]
        public string Language { get; set; }
        public string Image { get; set; }

        [Required (ErrorMessage = "You need to enter a title")]
        public string Title { get; set; }
        [Required (ErrorMessage = "Select at least one genre")]
        public string Genre { get; set; }
        public string Info { get; set; }

        [Required (ErrorMessage = "You have to enter the author id")]
        public int? AuthorId { get; set; }
        [Required (ErrorMessage = "Type in the publisher")]
        public string Publisher { get; set; }
        public int PageCount { get; set; }
        [Required (ErrorMessage = "Type in the release year")]
        public int? ReleaseYear { get; set; }

        [Required (ErrorMessage = "You must enter a price")]
        [Range(0, double.MaxValue, ErrorMessage = "A book cant have a negative price")]
        public double? Price { get; set; }
    }
}