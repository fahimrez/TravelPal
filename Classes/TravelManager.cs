using System.Collections.Generic;

namespace TravelPal.Classes
{
    public class TravelManager
    {
        // New list to remove or add travel destinations

        public List<Travel> travels = new();
        public void AddTravel(Travel travel)
        {
            travels.Add(travel);
        }

        // Method to remove a travel from the list
        public void RemoveTravel(Travel travel)
        {
            travels.Remove(travel);

            if (travel is Vacation)
            {
                travels.Remove(travel as Vacation);
            }
            else if (travel is Trip)
            {
                travels.Remove(travel as Trip);
            }
        }
    }
}
