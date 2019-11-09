using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveriesApp.Shared.Model
{
    #region Enums

    public enum UserStatus
    {
        UserExists,
        SuccessfullyRegistered,
        SuccessfullyLoggedIn,
        WrongCredentials,
        UnsuccessfullyRegistered,
        UnsuccessfullyLoggedIn
    }

    #endregion Enums

    public class User
    {
        #region Properties

        public string ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        #endregion Properties

        #region Methods

        public static async Task<UserStatus> Login(string email, string password)
        {
            UserStatus status = UserStatus.UnsuccessfullyLoggedIn;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                Database.Connection.CreateTable<User>();
                var data = Database.Connection.Table<User>();

                User existingUser = null;

                //If we have rows in users table
                if (Database.Connection.Table<User>().Any())
                {
                    existingUser = data.FirstOrDefault(user => user.Email == email);
                }

                //If user exist login user and password match with one in database
                if (existingUser != null &&
                    string.CompareOrdinal(password, existingUser.Password.Trim()) == 0)
                {

                    status = UserStatus.SuccessfullyLoggedIn;
                }
                else
                {
                    status = UserStatus.WrongCredentials;
                }
            }

            return status;
        }

        public static async Task<UserStatus> Register(string email, string password, string confirmPassword)
        {
            UserStatus status = UserStatus.UnsuccessfullyRegistered;

            Database.Connection.CreateTable<User>();
            var data = Database.Connection.Table<User>();

            User existingUser = null;

            //If we have rows in users table
            if (Database.Connection.Table<User>().Any())
            {
                existingUser = data.FirstOrDefault(user => user.Email == email);
            }

            //If user exists return toast message
            if (existingUser != null)
            {
                status = UserStatus.UserExists;
            }

            //If password matches with confirm password register user
            if (String.CompareOrdinal(password, confirmPassword) == 0)
            {
                var user = new User()
                {
                    Email = email,
                    Password = password
                };

                //Database.Connection.Insert(user);
                Database.Insert(user);
                status = UserStatus.SuccessfullyRegistered;
            }

            return status;
        }

        #endregion Methods
    }
}
