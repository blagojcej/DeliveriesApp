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
using DeliveriesApp.Shared.Model;

namespace DeliveriesApp
{
    [Activity(Label = "NewDeliveryActivity")]
    public class NewDeliveryActivity : Activity
    {
        private Button saveButton;
        private EditText packageNameEditText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.new_delivery);

            saveButton = FindViewById<Button>(Resource.Id.saveButton);
            packageNameEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);

            saveButton.Click += SaveButton_Click;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Delivery newDelivery=new Delivery()
            {
                Name = packageNameEditText.Text.Trim(),
                Status = 0
            };

            Delivery.InsertDelivery(newDelivery);
        }
    }
}