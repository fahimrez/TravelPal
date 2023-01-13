using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TravelPal.Enums;

namespace TravelPal.Classes
{
    public class UserManager
    {
        public List<IUser> Users { get; set; } = new();

        public IUser UserSignedIn { get; set; }


        // Admin manager
        public UserManager()
        {
            AdminNewUser();
        }

        // Load all users
        public List<IUser> LoadAllUsers()
        {
            return Users;
        }

        // Admin access to all user destinations
        public List<Travel> AdminGetTravels()
        {
            List<Travel> travels = new List<Travel>();
            List<User> users = new List<User>();

            foreach (IUser user in Users)
            {
                if (user is User)
                {
                    users.Add((User)user);
                }

            }

            foreach (User user in users)
            {
                foreach (Travel travel in user.Travels)
                {
                    travels.Add(travel);
                }

            }
            return travels;
        }

        // Admin sign in
        public void AdminNewUser()
        {
            Admin admin = new Admin
            {
                Username = "Admin",
                Password = "password",
                Location = Countries.Sweden
            };
            Users.Add(admin);
        }

        // Add User 
        public bool AddUser(string username, string password, string confrimPassword, Countries country)
        {
            if (ValidateUserName(username))
            {
                User newUser = new User
                {
                    Username = username,
                    Password = password,
                    Location = country,
                    Travels = new()
                };

                Users.Add(newUser);

                return true;
            }
            return false;
        }

        // Remove User 
        public bool RemoveUser(User user)
        {
            if (Users.Any(x => x.Username == user.Username))
            {
                Users.Remove(user);
                return true;
            }
            return false;

        }
        // Admin remove travel
        public bool AdminRemoveTravels(int index, string userName)
        {
            List<Travel> travelList = new();
            foreach (User user in Users.Where(x => x.GetType() == typeof(User)).ToList())
            {
                foreach (Travel travel in user.Travels)
                    travelList.Add(travel);
            }
            Travel travelToRemove = travelList.ElementAt(index);
            User userTravelToDelete = (User)Users.FirstOrDefault(x => x.Username == userName);
            userTravelToDelete.Travels.Remove(travelToRemove);
            return true;
        }

        // Check if sign-in details are within correct length
        public bool ValidateUserName(string username)
        {
            return username.Length > 20 || username.Length < 3 ? false : true;

        }
        private bool ValidatePassword(string password)
        {
            return password.Length > 20 || password.Length < 5 ? false : true;

        }
        // Update password
        public bool UpdatePassword(User user, string updatedPassword)
        {
            user.Password = updatedPassword;
            return true;
        }
        // Update username 
        public bool UpdateUserName(User user, string updatedUserName)
        {
            user.Username = updatedUserName;
            return true;
        }

        // Sign in user
        public bool UserSignIn(string username, string password)
        {
            foreach (IUser user in Users)
            {
                if (user.Username == username && user.Password == password)
                {
                    UserSignedIn = user;
                    return true;
                }
            }
            return false;
        }
        // Update location
        public bool UpdateCountry(User user, Countries country)
        {
            user.Location = country;
            return true;
        }

        // Check if sign-in details are within correct length
        public bool CheckUserLength(string newName)
        {

            if (newName.Length < 5 || newName.Length > 10)
            {
                MessageBox.Show("The username must be between 5 and 10 characters long!", "Warning");
                return false;
            }
            return true;
        }


        // Check if username is already in use
        public bool ValidateUsername(string username)
        {
            foreach (IUser user in Users)
            {
                if (user.Username == username)
                {
                    MessageBox.Show("That username is already in use!", "Warning!");
                    return false;
                }
            }
            return true;
        }

        // Check if passwords are the same
        public bool ConfirmNewPassword(string pass, string confirm)
        {
            if (pass == confirm)
            {
                return true;
            }
            MessageBox.Show("Your password does not match.", "Warning!");
            return false;
        }

        // Check new password length
        public bool CheckNewPasswordLength(string pass)
        {

            if (pass.Length < 5 || pass.Length > 16)
            {
                MessageBox.Show("Password needs to be between 5 and 16 characters.", "Warning!");
                return false;
            }
            return true;
        }



    }
}
