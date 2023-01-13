using System.Collections.Generic;
using TravelPal.Enums;

namespace TravelPal.Classes
{
    public class User : IUser
    {
        // Declare and initialize correct variables

        public List<Travel> Travels { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Countries Location { get; set; }


        public User()
        {

        }

        public User(string username, string password, Countries locations)
        {
            Username = username;
            Password = password;
            Location = locations;

            Travels = new();
        }
    }
}
