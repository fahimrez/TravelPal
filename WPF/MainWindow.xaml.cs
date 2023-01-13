using System.Windows;
using TravelPal.Classes;
using TravelPal.Enums;

namespace TravelPal.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // New usermanager list
        private UserManager userManager = new();


        //Adds Gandalf as admin
        public MainWindow()
        {
            InitializeComponent();
            User userGandalf = new("Gandalf", "password", Countries.Sweden);
            userGandalf.Travels.Add(new Trip(TripTypes.Work, "Malmö", Countries.Sweden, 6));
            userGandalf.Travels.Add(new Vacation(true, "Berlin", Countries.Germany, 9));
            userManager.Users.Add(userGandalf);
        }

        // Open registerwindow
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Window registerWindow = new RegisterWindow(userManager);
            registerWindow.Show();
        }

        // Sign in user and check if there already is a user with same details
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {

            string username = txbUsername.Text;
            string password = pswPassword.Password;
            string confirmPassword = pswConfirmPassword.Password;

            bool isFoundUSer = false;
            foreach (IUser user in userManager.Users)
            {
                if (user.Username == username && user.Password == password && user.Password == confirmPassword)
                {
                    isFoundUSer = true;
                    userManager.UserSignedIn = user;

                    if (user is User)
                    {
                        TravelsWindow travelsWindow = new(userManager, (User)user);
                        travelsWindow.Show();
                        Hide();
                        break;
                    }
                    else if (user is Admin)
                    {
                        TravelsWindow travelswindow = new(userManager, (Admin)user);
                        travelswindow.Show();
                        Hide();
                        break;
                    }
                }
            }
            if (!isFoundUSer)
            {
                MessageBox.Show("Username or password is incorrect");
                return;
            }
        }
    }
}
