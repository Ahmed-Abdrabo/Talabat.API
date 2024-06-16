namespace Talabat.Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string AppUserId { get; set; }   // Foreign Key => User
        public AppUser User { get; set; }
        public string Country { get; set; }
    }
}