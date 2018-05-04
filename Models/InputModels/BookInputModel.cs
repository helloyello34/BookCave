using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class BookInputModel
    {
        public int Id { get; set; }

      ///  [Required (ErrorMessage = "You need to enter the ISBN number")] 
        public string ISBN { get; set; }
        public string Language { get; set; }
        public string Image { get; set; }

      ///  [Required (ErrorMessage = "You need to enter a title")]
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Info { get; set; }

      ///  [Required (ErrorMessage = "You have to enter the author id")]
        public int AuthorId { get; set; }
        public string Publisher { get; set; }
        public int PageCount { get; set; }
        
     ///   [Required (ErrorMessage = "You must enter a release year")]
       /// [Range (1900,2030, ErrorMessage = "Release year must be between 1900 - 2030")]
        public int ReleaseYear { get; set; }

      ///  [Required (ErrorMessage = "You must enter a price")]
        public double Price { get; set; }
    }
}