namespace BookCave.Models.EntityModels
{
    public class Comment
    {
        public int     Id       { get; set; }
        public int     BookId   { get; set; }
        public string  Comments { get; set; }
        public double  Rating   { get; set; }

    }
}