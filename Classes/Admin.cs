using TravelPal.Enums;

namespace TravelPal.Classes
{
    public class Admin : IUser
    {
        // Declare and initialize correct variables for admin

        public string Username { get; set; }
        public string Password { get; set; }
        public Countries Location { get; set; }
    }
}
