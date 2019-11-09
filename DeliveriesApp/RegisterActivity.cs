using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using DeliveriesApp.Shared.Model;
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

        private async void RegisterUserButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(registerEmailEditText.Text.Trim()) &&
                !string.IsNullOrEmpty(registerPasswordEditText.Text.Trim()) &&
                !string.IsNullOrEmpty(registerConfirmPasswordEditText.Text.Trim()))
            {
                var email = registerEmailEditText.Text.Trim();
                var password = registerPasswordEditText.Text.Trim();
                var confirmPassword = registerConfirmPasswordEditText.Text.Trim();

                var result = await User.Register(email, password, confirmPassword);

                switch (result)
                {
                    case UserStatus.UserExists:
                        Toast.MakeText(this, "User already exists", ToastLength.Long).Show();
                        break;
                    case UserStatus.SuccessfullyRegistered:
                        Toast.MakeText(this, "Successfully registered", ToastLength.Long).Show();
                        break;
                    default:
                        Toast.MakeText(this, "Unknown Error", ToastLength.Long).Show();
                        break;
                }
            }
        }
    }
}