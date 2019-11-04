using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Linq;

namespace DeliveriesApp
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        private EditText registerEmailEditText, registerPasswordEditText, registerConfirmPasswordEditText;
        private Button registerUserButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.register);

            registerEmailEditText = FindViewById<EditText>(Resource.Id.registerEmailEditText);
            registerPasswordEditText = FindViewById<EditText>(Resource.Id.registerPasswordEditText);
            registerConfirmPasswordEditText = FindViewById<EditText>(Resource.Id.registerConfirmPasswordEditText);
            registerUserButton = FindViewById<Button>(Resource.Id.registerUserButton);

            registerUserButton.Click += RegisterUserButton_Click;

            string email = Intent.GetStringExtra("email");
            registerEmailEditText.Text = email;
        }

        private void RegisterUserButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(registerEmailEditText.Text.Trim()) &&
                !string.IsNullOrEmpty(registerPasswordEditText.Text.Trim()) &&
                !string.IsNullOrEmpty(registerConfirmPasswordEditText.Text.Trim()))
            {
                Database.Connection.CreateTable<User>();

                User existingUser = null;

                //If we have rows in users table
                if (Database.Connection.Table<User>().Any())
                {
                    existingUser = Database.Connection.Get<User>(user => user.Email == registerEmailEditText.Text);
                }

                //If user exists return toast message
                if (existingUser != null)
                {
                    Toast.MakeText(this, "User already exists", ToastLength.Long).Show();
                    return;
                }

                //If password matches with confirm password register user
                if (String.CompareOrdinal(registerPasswordEditText.Text, registerConfirmPasswordEditText.Text)==0)
                {
                    var user=new User()
                    {
                        Email = registerEmailEditText.Text.Trim(),
                        Password = registerPasswordEditText.Text.Trim()
                    };

                    Database.Connection.Insert(user);
                    Toast.MakeText(this, "Successfully registered", ToastLength.Long).Show();
                    return;
                }

                Toast.MakeText(this, "Password's don't match", ToastLength.Long).Show();
                return;
            }

            Toast.MakeText(this, "Email or Password can not be empty", ToastLength.Long).Show();
        }
    }
}