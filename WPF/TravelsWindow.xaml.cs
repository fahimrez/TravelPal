using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TravelPal.Classes;

namespace TravelPal.WPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        private List<Travel> travels = new();
        private MainWindow _mainWindow;
        private UserManager userManager;
        private TravelManager travelManager;
        private User _user;
        private Admin admin;
        private bool IsAdmin = false;


        public TravelsWindow(UserManager userManager, IUser user)
        {
            InitializeComponent();
            this.userManager = userManager;
            lbTravelsUserName.Content = userManager.UserSignedIn.Username;

            if (userManager.UserSignedIn is User)
            {
                _user = (User)userManager.UserSignedIn;
                travels = _user.Travels;
                ShowTravels();
            }

            if (userManager.UserSignedIn is Admin)
            {
                btnTravelsAddTravel.IsEnabled = false;
                btnTravelsUserDetails.IsEnabled = false;
                admin = (Admin)user;
                lbTravelsUserName.Content = "Signed in as ADMIN";
                ShowTravels();
                IsAdmin = true;
            }

        }

        // Open userwindow
        private void btnTravelsUserDetails_Click(object sender, RoutedEventArgs e)
        {
            Window userDetailsWindow = new UserDetailsWindow(userManager);
            userDetailsWindow.Show();
            Close();

        }

        // Open travelswindow to add a travel
        private void btnTravelsAddTravel_Click(object sender, RoutedEventArgs e)
        {
            Window addTravelWindow = new AddTravelWindow(_user, this);
            addTravelWindow.Show();
        }

        //Method to remove a destination
        private void btnTravelsRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lwTravelsInfo.SelectedIndex is -1)
            {
                MessageBox.Show("Select a destination to remove first.");
                return;
            }
            Travel travelToRemove = travels[lwTravelsInfo.SelectedIndex];

            if (travelToRemove is Trip)
                travels.Remove((Trip)travelToRemove);
            else
                travels.Remove((Vacation)travelToRemove);
            ShowTravels();
        }
        // Opens travelwindow and if nothing is marked, a warning message will pop up.
        private void btnTravelsDetails_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = lwTravelsInfo.SelectedItem as ListViewItem;
            if (selectedItem != null)
            {
                Travel selectedTravel = selectedItem.Tag as Travel;
                TravelDetailsWindow travelDetailsWindow = new(userManager, travelManager, selectedTravel);
                travelDetailsWindow.Show();
            }
            else
            {
                MessageBox.Show("Please choose a destination to remove from the list!");
            }
        }

        // An infobox that explains the application
        private void btnTravelsInfo_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show("Welcome! This application will help you find your dream vacation. All destinations are included.");
        }

        // Exit window and opens mainwindow
        private void btnTravelsSignOut_Click(object sender, RoutedEventArgs e)
        {
            

            userManager.UserSignedIn = null;
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }

        // Show travel with userinfo, travellers and destination
        public void ShowTravels()
        {
            lwTravelsInfo.Items.Clear();
            if (IsAdmin)
            {
                List<IUser> userList = this.userManager.Users.Where(x => x.GetType() == typeof(User)).ToList();
                foreach (User user in userList)
                {
                    foreach (Travel travel in user.Travels)
                    {
                        travels.Add(travel);
                        ListViewItem listViewItem = new();
                        listViewItem.Tag = travel;
                        listViewItem.Content = $"User: {user.Username} Trip: {travel.Destination} | Country: {travel.Country.ToString()} | Travelers: {travel.Travellers.ToString()}";
                        lwTravelsInfo.Items.Add(listViewItem);
                    }
                }
            }
            else
            {
                foreach (Travel travel in travels)
                {
                    ListViewItem listViewItem = new();
                    listViewItem.Tag = travel;
                    listViewItem.Content = $"Travel: {travel.Destination} | Country: {travel.Country.ToString()} | Travelers: {travel.Travellers.ToString()}";
                    lwTravelsInfo.Items.Add(listViewItem);
                }
            }
        }


    }
}
