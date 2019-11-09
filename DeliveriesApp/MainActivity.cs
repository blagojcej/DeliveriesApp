using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Shared.Model;

namespace DeliveriesApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText emailEditText;
        private EditText passwordEditText;
        private Button signInButton;
        private Button registerButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            signInButton = FindViewById<Button>(Resource.Id.signInButton);
            registerButton = FindViewById<Button>(Resource.Id.registerButton);

            signInButton.Click += SignInButton_Click;
            registerButton.Click += RegisterButton_Click;
        }

        private void RegisterButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(RegisterActivity));

            //Pass information to register activity
            intent.PutExtra("email", emailEditText.Text);

            StartActivity(intent);
        }

        private async void SignInButton_Click(object sender, System.EventArgs e)
        {
            var email = emailEditText.Text.Trim();
            var password = passwordEditText.Text.Trim();

            var result = await User.Login(email, password);

            switch (result)
            {
                case UserStatus.SuccessfullyLoggedIn:
                    Toast.MakeText(this, "Login succesfull", ToastLength.Long).Show();
                    break;
                case UserStatus.WrongCredentials:
                    Toast.MakeText(this, "Incorrect email or password", ToastLength.Long).Show();
                    break;
                default:
                    Toast.MakeText(this, "Unknown Error", ToastLength.Long).Show();
                    break;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
	}
}

