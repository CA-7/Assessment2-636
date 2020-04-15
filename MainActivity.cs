using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;

namespace Task3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback
    {
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.item1);
            Button reg = FindViewById<Button>(Resource.Id.button3);
            reg.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(Register));
                StartActivity(intent);
            };

            /*GoogleMapOptions mapOptions = new GoogleMapOptions()
                 .InvokeMapType(GoogleMap.MapTypeNormal)
                 .InvokeZoomControlsEnabled(false)
                 .InvokeCompassEnabled(true);*/

            var mapFrag = MapFragment.NewInstance(); // MapOptions

            FragmentManager.BeginTransaction()
                                    .Add(Resource.Id.mapFragContainer, mapFrag, "map_fragment")
                                    .Commit();

            //var mapFragement = (MapFragment)FragemenrManager.FindFragementById(Resource.Id.mapfrag);
            mapFrag.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            googleMap.MapType = GoogleMap.MapTypeNormal;
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            getCurrentLoc(googleMap);
        }
      
        public async void getLastLocation(GoogleMap googleMap)
        {
            Console.WriteLine("Test - LastLoc");
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    Console.WriteLine($"Last Loc - Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    MarkerOptions curLoc = new MarkerOptions();
                    curLoc.SetPosition(new LatLng(location.Latitude, location.Longitude));
                    var address = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = address?.FirstOrDefault();
                    var geocodeAddress = "";
                    if (placemark != null)
                    {
                        geocodeAddress =
                            $"AdminArea:       {placemark.AdminArea}\n" +
                            $"CountryCode:     {placemark.CountryCode}\n" +
                            $"CountryName:     {placemark.CountryName}\n" +
                            $"FeatureName:     {placemark.FeatureName}\n" +
                            $"Locality:        {placemark.Locality}\n" +
                            $"PostalCode:      {placemark.PostalCode}\n" +
                            $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                            $"SubLocality:     {placemark.SubLocality}\n" +
                            $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                            $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    }
                    curLoc.SetTitle("You were here" + geocodeAddress);
                    curLoc.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueAzure));
                    googleMap.AddMarker(curLoc);
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
                Toast.MakeText(this, "Feature Not Supported", ToastLength.Short);
            }
            catch (FeatureNotEnabledException)
            {
                // Handle not enabled on device exception
                Toast.MakeText(this, "Feature Not Enabled", ToastLength.Short);
            }
            catch (PermissionException)
            {
                // Handle permission exception
                Toast.MakeText(this, "Needs more permission", ToastLength.Short);
            }
            catch (Exception)
            {
                // Unable to get location
                Toast.MakeText(this, "Unable to get location", ToastLength.Short);
            }
        }

        public async void getCurrentLoc(GoogleMap googleMap)
        {
            Console.WriteLine("Test - CurrentLoc");
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"current Loc - Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    MarkerOptions curLoc = new MarkerOptions();
                    curLoc.SetPosition(new LatLng(location.Latitude, location.Longitude));


                    var address = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = address?.FirstOrDefault();
                    var geocodeAddress = "";
                    if (placemark != null)
                    {
                        geocodeAddress =
                            $"AdminArea:       {placemark.AdminArea}\n" +
                            $"CountryCode:     {placemark.CountryCode}\n" +
                            $"CountryName:     {placemark.CountryName}\n" +
                            $"FeatureName:     {placemark.FeatureName}\n" +
                            $"Locality:        {placemark.Locality}\n" +
                            $"PostalCode:      {placemark.PostalCode}\n" +
                            $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                            $"SubLocality:     {placemark.SubLocality}\n" +
                            $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                            $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    }


                    curLoc.SetTitle("You are here");// + geocodeAddress);
                    curLoc.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueAzure));

                    googleMap.AddMarker(curLoc);


                    CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
                    builder.Target(new LatLng(location.Latitude, location.Longitude));
                    builder.Zoom(18);
                    builder.Bearing(155);
                    builder.Tilt(65);

                    CameraPosition cameraPosition = builder.Build();

                    CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

                    googleMap.MoveCamera(cameraUpdate);
                }
                else
                {
                    getLastLocation(googleMap);
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
                Toast.MakeText(this, "Feature Not Supported", ToastLength.Short);
            }
            catch (FeatureNotEnabledException)
            {
                // Handle not enabled on device exception
                Toast.MakeText(this, "Feature Not Enabled", ToastLength.Short);
            }
            catch (PermissionException)
            {
                // Handle permission exception
                Toast.MakeText(this, "Needs more permission", ToastLength.Short);
            }
            catch (Exception)
            {
                getLastLocation(googleMap);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}