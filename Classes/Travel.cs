using TravelPal.Enums;

namespace TravelPal.Classes
{
    public class Travel
    {
        // Declare and initialize correct variables
        public string Destination { get; set; }
        public Countries Country { get; set; }
        public int Travellers { get; set; }

        public Travel(string destination, Countries country, int travellers)
        {
            Destination = destination;
            Country = country;
            Travellers = travellers;
        }

        
        public virtual string GetInfo()
        {
            return $"{Destination}";
        }
    }
}
