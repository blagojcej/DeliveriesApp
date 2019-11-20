using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Shared.Model;

namespace DeliveriesApp
{
    public class DeliveredFragment : Android.Support.V4.App.ListFragment //To support older versions of android
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            var delivered = Delivery.GetDelivered();
            //We can not use this as Context in fragment because it needs to be an activity
            //ListAdapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, delivered);
            ListAdapter=new DeliveryAdapter(Activity, delivered);
        }
    }
}