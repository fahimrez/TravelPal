using System.Windows;
using TravelPal.Classes;

namespace TravelPal.WPF
{
    /// <summary>
    /// Interaction logic for TravelDetailsWindow.xaml
    /// </summary>
    public partial class TravelDetailsWindow : Window
    {
        private UserManager usermanager;
        private User user;
        private readonly UserManager userManager;
        private readonly TravelManager travelManager;
        private Travel vacation;
        AddTravelWindow addTravelWindow;

        public TravelDetailsWindow(UserManager userManger, TravelManager travelManager, Travel travel)
        {
            InitializeComponent();

            this.userManager = userManger;
            this.travelManager = travelManager;
            this.vacation = travel;



            // Vacation or trip with different visibility functions 
            if (travel is Trip)
            {
                Trip trip = travel as Trip;

                string tripType = trip.TripType.ToString();

                tbTravelDetailsDestination.Text = trip.Destination;
                tbTravelDetailsCountry.Text = trip.Country.ToString();
                tbTravelDetailsTravelers.Text = trip.Travellers.ToString();
                tbTravelDetailsTrip.Text = "Trip";
                tbTravelDetailsTripType.Visibility = Visibility.Visible;
                tbTravelDetailsTripType.Text = "Trip";
                tbTravelDetailsTrip.Text = tripType;
                lblTravelDetailsTrpType.Visibility = Visibility.Visible;
                cbTravelDetailsAllInclusive.Visibility = Visibility.Hidden;

            }
            else if (travel is Vacation)
            {
                Vacation vacation = travel as Vacation;

                if (vacation != null && vacation.AllInclusive)
                {
                    vacation.AllInclusive = true;
                    cbTravelDetailsAllInclusive.IsChecked = vacation.AllInclusive;
                    tbTravelDetailsDestination.Text = vacation.Destination;
                    tbTravelDetailsCountry.Text = vacation.Country.ToString();
                    tbTravelDetailsTravelers.Text = vacation.Travellers.ToString();
                    tbTravelDetailsTrip.Text = "vacation";
                    tbTravelDetailsTripType.Visibility = Visibility.Hidden;
                    lblTravelDetailsTrpType.Visibility = Visibility.Hidden;
                    cbTravelDetailsAllInclusive.IsChecked = vacation.AllInclusive;
                }
                else if (vacation != null && !vacation.AllInclusive)
                {
                    cbTravelDetailsAllInclusive.IsChecked = false;

                    vacation.AllInclusive = false;
                    tbTravelDetailsDestination.Text = vacation.Destination;
                    tbTravelDetailsCountry.Text = vacation.Country.ToString();
                    tbTravelDetailsTravelers.Text = vacation.Travellers.ToString();
                    tbTravelDetailsTrip.Text = "vacation";
                    tbTravelDetailsTripType.Visibility = Visibility.Hidden;
                    lblTravelDetailsTrpType.Visibility = Visibility.Hidden;
                    cbTravelDetailsAllInclusive.IsChecked = vacation.AllInclusive;
                    cbTravelDetailsAllInclusive.IsEnabled = false;
                }
            }
        }

        // Close window
        private void btnTravelDetailsExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

