using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

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
            
        }
    }
}