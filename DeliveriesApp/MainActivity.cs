using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

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

        private void SignInButton_Click(object sender, System.EventArgs e)
        {
            var email = emailEditText.Text.Trim();
            var password = passwordEditText.Text.Trim();

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                Database.Connection.CreateTable<User>();

                User existingUser = null;

                //If we have rows in users table
                if (Database.Connection.Table<User>().Any())
                {
                    existingUser = Database.Connection.Get<User>(user => user.Email == email);
                }

                //If user exist login user and password match with one in database
                if (existingUser != null &&
                    string.CompareOrdinal(password, existingUser.Password.Trim()) == 0)
                {

                    Toast.MakeText(this, "Login succesfull", ToastLength.Long).Show();
                    return;
                }

                Toast.MakeText(this, "Incorrect email or password", ToastLength.Long).Show();
                return;
            }

            Toast.MakeText(this, "Email or Password can not be empty", ToastLength.Long).Show();
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

