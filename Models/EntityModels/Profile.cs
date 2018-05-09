using System;

namespace BookCave.Models.EntityModels
{
    public class Profile
    {
        public string UserId    { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string Email     { get; set; }
        public string Country   { get; set; }
        public string City      { get; set; }
        public string Street    { get; set; }
        public string ZipCode   { get; set; }
        public string Phone     { get; set; }
        public string Gender    { get; set; }
        public DateTime Birthday { get; set; }
    }
}