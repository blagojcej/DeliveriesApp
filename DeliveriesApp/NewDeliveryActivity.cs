using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Shared.Model;

namespace DeliveriesApp
{
    [Activity(Label = "NewDeliveryActivity")]
    public class NewDeliveryActivity : Activity, IOnMapReadyCallback, ILocationListener
    {
        private Button saveButton;
        private EditText packageNameEditText;
        private MapFragment destinationMapFragment;
        private double latitute, longitude;
        //Location Manager handle changes in the users location and let us know about that changes
        private LocationManager locationManager;
        private GoogleMap gMap;
        private Location currentLocation;

        private void SaveButton_Click(object sender, EventArgs e)
        {

            var destinationLocation = gMap.CameraPosition.Target;
            Delivery newDelivery = new Delivery()
            {
                Name = packageNameEditText.Text.Trim(),
                Status = 0,
                OriginLatitude = currentLocation.Latitude,
                OriginLongitude = currentLocation.Longitude,
                DestinationLatitude = destinationLocation.Latitude,
                DestinationLongitude = destinationLocation.Longitude
            };

            Delivery.InsertDelivery(newDelivery);
        }

        #region Override

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.new_delivery);

            saveButton = FindViewById<Button>(Resource.Id.saveButton);
            packageNameEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);
            destinationMapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.destinationMapFragment);

            //Latitude and logitude may not have values yet
            //mapFragment.GetMapAsync(this);

            saveButton.Click += SaveButton_Click;

            //Moved to OnResume
            ////Location service is system service which always interact with hardware
            //locationManager = GetSystemService(LocationService) as LocationManager;
            //string provider = LocationManager.GpsProvider;

            ////Check if provider is available, does device has gps and does gps i turned on
            //if (locationManager.IsProviderEnabled(provider))
            //{
            //    locationManager.RequestLocationUpdates(provider, 5000, 1, this);
            //}
        }

        protected override void OnPause()
        {
            base.OnPause();
            //Stop listening for location changes whan app goes in background
            locationManager.RemoveUpdates(this);
        }

        protected override void OnResume()
        {
            base.OnResume();
            //Start listening for location changes when app is activated again
            //Location service is system service which always interact with hardware
            locationManager = GetSystemService(LocationService) as LocationManager;
            string provider = LocationManager.GpsProvider;

            //Check if provider is available, does device has gps and does gps i turned on
            if (locationManager.IsProviderEnabled(provider))
            {
                locationManager.RequestLocationUpdates(provider, 5000, 1, this);
            }

            //Get initial location
            //Set new provider (NetworkProvider), not very accured, just to set initial location
            var location = locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
            latitute = location.Latitude;
            longitude = location.Longitude;
            destinationMapFragment.GetMapAsync(this);
        }

        #endregion Override

        #region IOnMapReadyCallback

        public void OnMapReady(GoogleMap googleMap)
        {
            gMap = googleMap;

            //Add pin to map
            MarkerOptions marker=new MarkerOptions();
            marker.SetPosition(new LatLng(latitute, longitude));
            marker.SetTitle("Your Location");
            googleMap.AddMarker(marker);

            //CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            //builder.Target(marker.Position);
            //builder.Zoom(18);
            //builder.Bearing(155);
            //builder.Tilt(65);
            //CameraPosition cameraPosition = builder.Build();
            //CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            //googleMap.MoveCamera(cameraUpdate);

            googleMap.MoveCamera(CameraUpdateFactory.NewLatLng(new LatLng(latitute, longitude)));
        }

        #endregion IOnMapReadyCallback

        #region ILocationListener

        public void OnLocationChanged(Location location)
        {
            latitute = location.Latitude;
            longitude = location.Longitude;
            //To be sure latitude and logitude are set
            destinationMapFragment.GetMapAsync(this);
            currentLocation = location;
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
            
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            
        }

        #endregion ILocationListener
    }
}