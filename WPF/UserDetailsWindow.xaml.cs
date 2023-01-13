using System;
using System.Windows;
using TravelPal.Classes;
using TravelPal.Enums;

namespace TravelPal.WPF
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        private UserManager userManager;
        private IUser user;


        public UserDetailsWindow(UserManager userManager)
        {
            InitializeComponent();
            this.userManager = userManager;
            cmbUserDetailsCountry.ItemsSource = Enum.GetValues(typeof(Countries));
            cmbUserDetailsCountry.SelectedIndex = 0;
        }

  

        // Save a new user and then open travelswindow
        private void btnUserDetailsSave_Click(object sender, RoutedEventArgs e)
        {
            Countries selectedCountry = (Countries)Enum.Parse(typeof(Countries), cmbUserDetailsCountry.SelectedItem.ToString());

            if (userManager.ValidateUserName(txtUserDetailsUsername.Text) && userManager.ValidateUsername(txtUserDetailsUsername.Text) && userManager.ConfirmNewPassword(txtUserDetailsNewPassword.Text, txtUserDetailsConfirmPassword.Text) && userManager.CheckNewPasswordLength(txtUserDetailsNewPassword.Text))
            {
                userManager.UserSignedIn.Username = txtUserDetailsUsername.Text;
                userManager.UserSignedIn.Password = txtUserDetailsNewPassword.Text;
            }
            else
            {
                return;
            }
            userManager.UserSignedIn.Location = selectedCountry;
            TravelsWindow travelsWindow = new(userManager, user);
            travelsWindow.Show();
            Close();
        }

        //A button to cancel userdetails window and open travelswindow
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            user = userManager.UserSignedIn;
            TravelsWindow travelsWindow = new(userManager, user);
            travelsWindow.Show();
            Close();
        }
    }
}
