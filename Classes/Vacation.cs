using TravelPal.Enums;

namespace TravelPal.Classes
{
    public class Vacation : Travel
    {
        public bool AllInclusive { get; set; }
        public Vacation(bool allInclusive, string destination, Countries country, int travellers) : base(destination, country, travellers)
        {
            AllInclusive = allInclusive;
        }

        // Return information
        public string GetInfo()
        {
            return "getInfo";
        }
    }
}
